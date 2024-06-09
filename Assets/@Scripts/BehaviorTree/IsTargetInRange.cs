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

    private bool isMove = false;

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
        {
            Debug.Log("����");
            return TaskStatus.Success;  // ���� ����;
        }
        if(Managers.Battle.isDone.Value)
        {
            Debug.Log("���� ����");
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
                // closestCollider.GetComponent<UnitAgent>().isTargeting = true;
                range += 30;
                target.Value = closestCollider.gameObject;  // ���� ����� Ÿ�� ����
                GetComponent<UnitAgent>().SetTarget(target.Value);
                return TaskStatus.Success;  // ���� ����
            }
        }
        else 
        {
            if(isMove == false)
            {
                isMove = true;
                animator.SetBool("isIdle", false);
                animator.SetBool("isRun", true);
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = 5f;
                Vector3 targetDestination = transform.position + transform.forward * 100;
                navMeshAgent.SetDestination(targetDestination);
                Debug.Log("������ ����");
            }
         
            //     transform.Translate(transform.forward * unitAgent.speed*Time.deltaTime);
          
            // var targetDestination = transform.position + transform.forward * 300;
            //     navMeshAgent.isStopped = false;
            //   navMeshAgent.SetDestination(targetDestination);



            // transform.Translate(Vector3.forward * Time.deltaTime * 3.0f); // Adjust speed as needed
        }
        
        return TaskStatus.Failure;  // ���� ����
    }
}
