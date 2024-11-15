using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;


public class UnitBase : MonoBehaviour, IDamageable
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    public UnitData unitData;
    public UnitBattleEffects unitBattleEffects;
    public Stat stat = new Stat();
    public bool isTeam ;
    public bool isTargeting = false;
    public bool isGoldMining = false;
    public bool isAreaAttack; // 단일 공격과 광역 공격을 구분하는 플래그
    public Define.AttackColliderType attackColliderType;

    public IDirectAttack directAttack;
    public IAreaAttack areaAttack;

    public LayerMask targetLayer;
    public SkinnedMeshRenderer unitRenderer;
    private Color originalColor;
    private Color hitColor = Color.red;
    private float colorChangeDuration = 0.5f;
    public bool showGizmos = false;
    private float gizmoDisplayTime = 0.5f;
    private float gizmoTimer;

    public bool isPlayable = false;
    Outline outline;
    virtual public void Start()
    {
        outline= gameObject.AddComponent<Outline>();
        OffOutline();
        if (isPlayable)
        {
            //저장된 플레이어의 스텟 읽어오기
            stat.damage = 1;
            stat.attackRadius = 5;
            stat.criticalRate = 0.5f;
            stat.criticalDamage = 1f;
        }
        else
        {
            stat.hp = unitData.hp;
            stat.moveSpeed = unitData.speed;
            stat.damage = unitData.damage;
            stat.attackRange = unitData.attackRange;
            stat.attackSpeed = unitData.attackSpeed;
            stat.attackRadius = unitData.attackRadius;
            stat.goldIncome = unitData.goldIncome;
        }


        if (unitRenderer != null)
        {
            originalColor = unitRenderer.material.color;
        }

        SetTargetLayer();

        if (isAreaAttack)
        {
            areaAttack = new AreaAttack { enemyLayer = targetLayer, attackColliderType = attackColliderType }; // 광역 공격 전략 설정
        }
        else
        {
            directAttack = new DirectAttack(); // 단일 공격 전략 설정
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmos && areaAttack != null)
        {
            ((AreaAttack)areaAttack).DrawGizmos(transform.position, stat.attackRadius);
        }
    }
    private void Update()
    {
        if (showGizmos)
        {
            gizmoTimer -= Time.deltaTime;
            if (gizmoTimer <= 0)
            {
                showGizmos = false;
            }
        }
    }
    public void ShowGizmosForAttack()
    {
        showGizmos = true;
        gizmoTimer = gizmoDisplayTime;
    }
    public void SetTargetLayer() 
    {
        int enemyLayer = LayerMask.NameToLayer(Define.TargetLayerType.Enemy.ToString());
        int teamLayer = LayerMask.NameToLayer(Define.TargetLayerType.Team.ToString());

        if (gameObject.layer == enemyLayer)
        {
            targetLayer = 1 << teamLayer; // 비트마스크로 변환
        }
        else if (gameObject.layer == teamLayer)
        {
            targetLayer = 1 << enemyLayer; // 비트마스크로 변환
        }
    }

    public void OnOutline(Outline.Mode mode,Color color, float width)
    {
        outline.OutlineMode = mode;
        outline.OutlineColor = color;
        outline.OutlineWidth = width;
    }
    public void OffOutline()
    {
        outline.OutlineMode = Outline.Mode.SilhouetteOnly;
    }
    public void OnDamage(Stat _stat)
    {
        float damageToApply = _stat.damage;
        bool isCritical = false;
        // 크리티컬 계산
        if (Random.value <= _stat.criticalRate)
        {
            damageToApply += (_stat.damage * _stat.criticalDamage);
            isCritical = true;
        }

        stat.hp -= damageToApply;
        Managers.Floating.OnFloatingDamage(transform, damageToApply, isCritical);
        StartCoroutine(ChangeColorOnHit());

        if (stat.hp <= 0)
        {
            if (isTeam)
                Managers.Battle.teamUnitList.Remove(this);
            else
                Managers.Battle.enemyUnitList.Remove(this);

            isTargeting = false;
            Destroy(gameObject);
        }
    }
    private IEnumerator ChangeColorOnHit()
    {
        if (unitRenderer != null)
        {
            unitRenderer.material.color = hitColor;
            yield return new WaitForSeconds(colorChangeDuration);
            unitRenderer.material.color = originalColor;
        }
    }

}
