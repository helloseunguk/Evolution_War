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
            navMeshAgent.speed = unitAgent.stat.moveSpeed;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (target.Value == null)
        {
         //   Debug.Log("FollowTarget1");
            animator.SetBool("isRun", false);
            return TaskStatus.Failure;  // Ÿ���� ������ ����
        }
      //  Debug.Log("FollowTarget2");
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
