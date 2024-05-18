using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FollowTarget : Action
{
    public SharedGameObject target;  // 따라갈 타겟 오브젝트
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public override void OnAwake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
            animator.SetBool("isRun", false);
            return TaskStatus.Failure;  // 타겟이 없으면 실패
        }
        navMeshAgent.isStopped = false;
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
