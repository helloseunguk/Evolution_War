using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTargetInRange : Conditional
{
    public string targetTag;  // 타겟의 태그
    public float range;       // 탐지 범위
    public SharedGameObject target;  // 탐지된 타겟 오브젝트
    public float checkInterval = 1.0f;  // 탐지 주기 (초 단위)
    private float nextCheckTime = 0.0f;

    public override TaskStatus OnUpdate()
    {
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag(targetTag))
                {
                    target.Value = collider.gameObject;  // 타겟 설정
                    GetComponent<UnitAgent>().SetTarget(target.Value);
                    return TaskStatus.Success;  // 조건 성공
                }
            }
        }

        return TaskStatus.Failure;  // 조건 실패
    }
}
