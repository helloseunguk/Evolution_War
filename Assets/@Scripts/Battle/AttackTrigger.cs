using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private int damage;
    private float lifeTime = 0.1f;
    private ObjectPool<AttackTrigger> objectPool;

    public void Initialize(int damage, Vector3 size, Vector3 offset, ObjectPool<AttackTrigger> pool)
    {
        this.damage = damage;
        this.objectPool = pool;
        SetTriggerBox(size, offset);
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void SetTriggerBox(Vector3 size, Vector3 offset)
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.size = size;
            boxCollider.center = offset;
        }
        else
        {
            Debug.LogWarning("BoxCollider not found on the AttackTrigger object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable targetHealth = other.GetComponent<IDamageable>();
        if (targetHealth != null)
        {
            Debug.Log("AttackTrigger의 데미지");
            targetHealth.OnDamage(damage);
        }
    }

    private void Deactivate()
    {
        objectPool.ReturnToPool(this);
    }
}
