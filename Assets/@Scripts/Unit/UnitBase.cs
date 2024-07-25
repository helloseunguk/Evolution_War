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

    public bool isAreaAttack; // ���� ���ݰ� ���� ������ �����ϴ� �÷���
    public float areaAttackRadius = 5f; // ���� ���� �ݰ�
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
            Debug.Log("Ÿ�� ���̾�" + targetLayer.value);
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
