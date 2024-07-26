using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsTargetInAttackRange : UnitConditional
{
    private float attackRange;

    public override void OnStart()
    {
        base.OnStart();
        attackRange = GetComponent<UnitBase>().stat.attackRange;
    }
    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            return TaskStatus.Failure;
        }
     
        if (Vector3.Distance(transform.position, target.Value.transform.position) <= attackRange)
        {
            navMeshAgent.isStopped = true;
            return TaskStatus.Success;

        }

        return TaskStatus.Failure;
    }
}
