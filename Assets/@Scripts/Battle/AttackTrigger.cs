using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private Stat stat;
    private float lifeTime = 0.1f;
    private ObjectPool<AttackTrigger> objectPool;

    public void Initialize(Stat _stat, Vector3 size, Vector3 offset, ObjectPool<AttackTrigger> pool)
    {
        stat = _stat;
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
            targetHealth.OnDamage(stat);
        }
    }

    private void Deactivate()
    {
        objectPool.ReturnToPool(this);
    }
}
