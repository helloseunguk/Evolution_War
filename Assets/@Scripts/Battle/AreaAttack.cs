using UnityEngine;
using static Define;

public class AreaAttack : IAreaAttack
{
    public LayerMask enemyLayer;
    public Define.AttackColliderType attackColliderType;

    public void OnAttack(Vector3 center, int damage, float radius)
    {
        if (attackColliderType == AttackColliderType.Box)
        {
            OnBoxAttack(center, damage, radius);
        }
        else if (attackColliderType == AttackColliderType.Sphere)
        {
            OnSphereAttack(center, damage, radius);
        }
    }

    private void OnBoxAttack(Vector3 center, int damage, float radius)
    {
        Vector3 halfExtents = new Vector3(radius, radius, radius);
        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("박스어택");
            IDamageable target = hitCollider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage);
            }
        }
    }

    private void OnSphereAttack(Vector3 center, int damage, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, enemyLayer);
        foreach (var hitCollider in hitColliders)
        {
            IDamageable target = hitCollider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(damage);
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
