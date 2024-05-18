using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsTargetInAttackRange : Conditional
{
    public SharedGameObject target;
    private float attackRange;
    private NavMeshAgent navMeshAgent;
    public override void OnAwake()
    {
        attackRange = GetComponent<UnitAgent>().unitData.attackRange;
        navMeshAgent = GetComponent<NavMeshAgent>();
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
