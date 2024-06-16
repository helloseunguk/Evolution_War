using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening.Core.Easing;

public class IsTargetInRange : UnitConditional
{
    public string targetTag;  // Ÿ���� �±�
    public float range;       // Ž�� ����

    public float checkInterval = 1.0f;  // Ž�� �ֱ� (�� ����)
    private float nextCheckTime = 0.0f;
    private float nextMoveTime = 0.0f;  // �̵� �ֱ� Ÿ�̸�

    private bool isMove = false;

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
        {
            return TaskStatus.Success;  // ���� ����
        }
        if (Managers.Battle.isDone.Value)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
            return TaskStatus.Failure;
        }

        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag(targetTag))
                {
                    isMove = false;
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = collider;
                    }
                }
            }
            if (closestCollider != null)
            {
                range += 30;
                target.Value = closestCollider.gameObject;  // ���� ����� Ÿ�� ����
                GetComponent<UnitAgent>().SetTarget(target.Value);
                return TaskStatus.Success;  // ���� ����
            }
        }

        // Move logic with interval check
        if (Time.time >= nextMoveTime)
        {
            nextMoveTime = Time.time + checkInterval;  // Set the next move time
            isMove = true;
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = unitAgent.speed;
            Vector3 targetDestination = transform.position + transform.forward * 10;
            navMeshAgent.SetDestination(targetDestination);
        }

        return TaskStatus.Failure;  // ���� ����
    }
}
