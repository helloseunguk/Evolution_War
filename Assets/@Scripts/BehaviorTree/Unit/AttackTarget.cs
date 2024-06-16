using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : UnitAction
{
    private float attackInterval = 1.0f;
    private float nextAttackTime = 0.0f;

    public override void OnAwake()
    {
        base.OnAwake();
        attackInterval = GetComponent<UnitAgent>().unitData.attackSpeed;
    }
    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            animator.ResetTrigger("onAttack");
            return TaskStatus.Failure;
        }

        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackInterval;

            UnitAgent targetAgent = target.Value.GetComponent<UnitAgent>();
            if (targetAgent != null)
            {
               // targetAgent.OnDamaged(GetComponent<UnitAgent>().damage);
                animator.SetTrigger("onAttack");
            }

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
