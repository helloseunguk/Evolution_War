using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class UnitBase : MonoBehaviour, IDamageable
{
    public UnitData unitData;
    public UnitBattleEffects unitBattleEffects;

    public int hp;
    public int speed;
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public bool isTeam ;
    public bool isTargeting = false;

    public bool isAreaAttack; // 단일 공격과 광역 공격을 구분하는 플래그
    public float areaAttackRadius = 5f; // 광역 공격 반경
    public Define.AttackColliderType attackColliderType;

    public IDirectAttack directAttack;
    public IAreaAttack areaAttack;

    LayerMask targetLayer;
    public SkinnedMeshRenderer unitRenderer;
    private Color originalColor;
    private Color hitColor = Color.red;
    private float colorChangeDuration = 0.5f;
    private bool showGizmos = false;
    private float gizmoDisplayTime = 0.5f;
    private float gizmoTimer;
    virtual public void Start()
    {
        hp = unitData.hp;
        speed = unitData.speed;
        damage = unitData.damage;
        attackRange = unitData.attackRange;
        attackSpeed = unitData.attackSpeed;
        if(unitRenderer != null)
        {
            originalColor = unitRenderer.material.color;
        }

        SetTargetLayer();

        if (isAreaAttack)
        {
            Debug.Log("타겟 레이어" + targetLayer.value);
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
            ((AreaAttack)areaAttack).DrawGizmos(transform.position, areaAttackRadius);
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
        Debug.Log("타겟레이어 진입" + gameObject.layer);
        int enemyLayer = LayerMask.NameToLayer(Define.TargetLayerType.Enemy.ToString());
        int teamLayer = LayerMask.NameToLayer(Define.TargetLayerType.Team.ToString());

        if (gameObject.layer == enemyLayer)
        {
            Debug.Log("타겟레이어 설정" + teamLayer);
            targetLayer = 1 << teamLayer; // 비트마스크로 변환
        }
        else if (gameObject.layer == teamLayer)
        {
            Debug.Log("타겟레이어 설정" + enemyLayer);
            targetLayer = 1 << enemyLayer; // 비트마스크로 변환
        }
    }
    public void OnDamage(int damage)
    {
        hp -= damage;
        StartCoroutine(ChangeColorOnHit());
        if (hp <= 0)
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
