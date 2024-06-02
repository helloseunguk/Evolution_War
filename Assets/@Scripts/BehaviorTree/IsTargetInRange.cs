using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTargetInRange : UnitConditional
{
    public string targetTag;  // Ÿ���� �±�
    public float range;       // Ž�� ����

    public float checkInterval = 1.0f;  // Ž�� �ֱ� (�� ����)
    private float nextCheckTime = 0.0f;

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null) return TaskStatus.Success;  // ���� ����;

        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;

           
            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag(targetTag) )
                {
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
                target.Value = closestCollider.gameObject;  // ���� ����� Ÿ�� ����
                GetComponent<UnitAgent>().SetTarget(target.Value);
                return TaskStatus.Success;  // ���� ����
            }
        }
        return TaskStatus.Failure;  // ���� ����
    }
}
