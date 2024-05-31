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
            return TaskStatus.Failure;  // 타겟이 없으면 실패
        }
        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        navMeshAgent.isStopped = false;
        animator.SetBool("isIdle", false);
        animator.SetBool("isRun", true);
        // 타겟을 따라가기 위해 목적지 설정
        navMeshAgent.SetDestination(target.Value.transform.position);
        return TaskStatus.Running;  // 계속 따라감
    }
    public override void OnEnd()
    {
        animator.SetBool("isRun", false);
    }
}
