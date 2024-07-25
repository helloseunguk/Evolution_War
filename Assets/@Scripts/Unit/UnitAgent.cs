using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitAgent : UnitBase
{
    public SharedGameObject target;
    IDamageable targetHealth;

    public void SetTarget(SharedGameObject newTarget)
    {
        target = newTarget;
        targetHealth = target.Value.GetComponent<IDamageable>();
    }

    public void ApplyDamage()
    {
        if (unitBattleEffects != null)
        {
            var hitEffect = unitBattleEffects.GetHitEffect();
            Vector3 targetCenterPosition = GetTargetCenterPosition(target.Value);
            hitEffect.gameObject.transform.position = targetCenterPosition;
            hitEffect.Play();
        }

        if (isAreaAttack)
        {
            Debug.Log("������ ����");
            Vector3 attackCenter = transform.position; // ���� �߽�
            areaAttack.OnAttack(attackCenter, damage, areaAttackRadius);
            ShowGizmosForAttack();
        }
        else
        {
            if (target.Value != null && targetHealth != null)
            {
                directAttack.OnAttack(targetHealth, damage);
            }
        }
    }

    private Vector3 GetTargetCenterPosition(GameObject target)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider != null)
        {
            return targetCollider.bounds.center;
        }

        Renderer targetRenderer = target.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            return targetRenderer.bounds.center;
        }

        return target.transform.position;
    }
}
