using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerTargetInRange : PlayerConditional
{
    public float detectionRange = 10f; // 탐지 범위
    public float detectionInterval = 0.1f; // 탐지 주기 (초 단위)
    private float lastDetectionTime; // 마지막 탐지 시간
    public LayerMask targetLayer; // 탐지할 대상의 레이어

    public string targetTag = "Enemy"; // 탐지할 대상의 태그

    private Transform agentTransform;

    public override void OnAwake()
    {
        base.OnAwake();
        agentTransform = transform; // 에이전트의 Transform 캐시
    }
    public override TaskStatus OnUpdate()
    {
        if (playerControl.movement.isMove.Value)
        {
            return TaskStatus.Failure;
        }
        if (Time.time - lastDetectionTime < detectionInterval)
        {
            return TaskStatus.Running; // 지정된 시간 간격이 지나지 않으면 Running 반환
        }
        lastDetectionTime = Time.time; // 마지막 탐지 시간 업데이트
        Collider[] hitColliders = Physics.OverlapSphere(agentTransform.position, detectionRange, targetLayer);
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag))
            {
                float distance = Vector3.Distance(agentTransform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = hitCollider;
                }
            }
        }

        if (closestCollider != null)
        {
            Debug.Log("타겟 설정");
            target.Value = closestCollider.gameObject; // 가장 가까운 적을 타겟으로 설정
            playerControl.targetUnit = target.Value.GetComponent<UnitBase>();
            return TaskStatus.Success; // 범위 내에 적이 있을 경우 Success 반환
        }
        target.Value = null;
        return TaskStatus.Failure; // 범위 내에 적이 없을 경우 Failure 반환
    }
}
