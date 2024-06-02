using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTargetInRange : UnitConditional
{
    public string targetTag;  // 타겟의 태그
    public float range;       // 탐지 범위

    public float checkInterval = 1.0f;  // 탐지 주기 (초 단위)
    private float nextCheckTime = 0.0f;

    public override TaskStatus OnUpdate()
    {
        if (target.Value != null) return TaskStatus.Success;  // 조건 실패;

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
                target.Value = closestCollider.gameObject;  // 가장 가까운 타겟 설정
                GetComponent<UnitAgent>().SetTarget(target.Value);
                return TaskStatus.Success;  // 조건 성공
            }
        }
        return TaskStatus.Failure;  // 조건 실패
    }
}
