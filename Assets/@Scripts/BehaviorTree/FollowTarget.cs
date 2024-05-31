using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FollowTarget : UnitAction
{

    public override void OnStart()
    {
        if (!navMeshAgent.enabled)
        {
            navMeshAgent.enabled = true;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
      
            animator.SetBool("isRun", false);
            return TaskStatus.Failure;  // Ÿ���� ������ ����
        }
        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        navMeshAgent.isStopped = false;
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", true);
        // Ÿ���� ���󰡱� ���� ������ ����
        navMeshAgent.SetDestination(target.Value.transform.position);
        return TaskStatus.Running;  // ��� ����
    }
    public override void OnEnd()
    {
        animator.SetBool("isRun", false);
    }
}
