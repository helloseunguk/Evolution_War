using DG.Tweening;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class UnitBase : MonoBehaviour, IDamageable
{
    public UnitData unitData;
    public UnitBattleEffects unitBattleEffects;
    public Stat stat = new Stat();
    public bool isTeam ;
    public bool isTargeting = false;

    public bool isAreaAttack; // ���� ���ݰ� ���� ������ �����ϴ� �÷���
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

    virtual public void Start()
    {
        if (isPlayable)
        {
            //����� �÷��̾��� ���� �о����
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
        }


        if (unitRenderer != null)
        {
            originalColor = unitRenderer.material.color;
        }

        SetTargetLayer();

        if (isAreaAttack)
        {
            areaAttack = new AreaAttack { enemyLayer = targetLayer, attackColliderType = attackColliderType }; // ���� ���� ���� ����
        }
        else
        {
            directAttack = new DirectAttack(); // ���� ���� ���� ����
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
        Debug.Log("Ÿ�ٷ��̾� ����" + gameObject.layer);
        int enemyLayer = LayerMask.NameToLayer(Define.TargetLayerType.Enemy.ToString());
        int teamLayer = LayerMask.NameToLayer(Define.TargetLayerType.Team.ToString());

        if (gameObject.layer == enemyLayer)
        {
            Debug.Log("Ÿ�ٷ��̾� ����" + teamLayer);
            targetLayer = 1 << teamLayer; // ��Ʈ����ũ�� ��ȯ
        }
        else if (gameObject.layer == teamLayer)
        {
            Debug.Log("Ÿ�ٷ��̾� ����" + enemyLayer);
            targetLayer = 1 << enemyLayer; // ��Ʈ����ũ�� ��ȯ
        }
    }

    public void OnDamage(Stat _stat)
    {
        float damageToApply = _stat.damage;
        bool isCritical = false;
        // ũ��Ƽ�� ���
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
