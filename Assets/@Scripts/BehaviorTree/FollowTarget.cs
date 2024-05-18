using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FollowTarget : Action
{
    public SharedGameObject target;  // ���� Ÿ�� ������Ʈ
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
            return TaskStatus.Failure;  // Ÿ���� ������ ����
        }
        navMeshAgent.isStopped = false;
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
