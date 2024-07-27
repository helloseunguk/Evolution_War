using UnityEngine;
using static Define;

public class AreaAttack : IAreaAttack
{
    public LayerMask enemyLayer;
    public Define.AttackColliderType attackColliderType;

    public void OnAttack(Vector3 center, Stat _stat)
    {
        if (attackColliderType == AttackColliderType.Box)
        {
            OnBoxAttack(center, _stat);
        }
        else if (attackColliderType == AttackColliderType.Sphere)
        {
            OnSphereAttack(center, _stat);
        }
    }

    private void OnBoxAttack(Vector3 center, Stat _stat)
    {
        Vector3 halfExtents = new Vector3(_stat.attackRadius, _stat.attackRadius, _stat.attackRadius);
        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("박스어택");
            IDamageable target = hitCollider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(_stat);
            }
        }
    }

    private void OnSphereAttack(Vector3 center, Stat _stat)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, _stat.attackRadius, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable target = hitCollider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(_stat);
            }
        }
    }

    public void DrawGizmos(Vector3 center, float radius)
    {
        if (attackColliderType == AttackColliderType.Box)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, new Vector3(radius * 2, radius * 2, radius * 2));
        }
        else if (attackColliderType == AttackColliderType.Sphere)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center, radius);
        }
    }
}
