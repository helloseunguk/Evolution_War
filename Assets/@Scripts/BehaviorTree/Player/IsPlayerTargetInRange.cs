using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerTargetInRange : PlayerConditional
{
    public float detectionRange = 10f; // Ž�� ����
    public float detectionInterval = 0.1f; // Ž�� �ֱ� (�� ����)
    private float lastDetectionTime; // ������ Ž�� �ð�
    public LayerMask targetLayer; // Ž���� ����� ���̾�

    public string targetTag = "Enemy"; // Ž���� ����� �±�

    private Transform agentTransform;

    public override void OnAwake()
    {
        base.OnAwake();
        agentTransform = transform; // ������Ʈ�� Transform ĳ��
    }
    public override TaskStatus OnUpdate()
    {
        if (playerControl.movement.isMove.Value)
        {
            return TaskStatus.Failure;
        }
        if (Time.time - lastDetectionTime < detectionInterval)
        {
            return TaskStatus.Running; // ������ �ð� ������ ������ ������ Running ��ȯ
        }
        lastDetectionTime = Time.time; // ������ Ž�� �ð� ������Ʈ
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
            Debug.Log("Ÿ�� ����");
            target.Value = closestCollider.gameObject; // ���� ����� ���� Ÿ������ ����
            playerControl.targetUnit = target.Value.GetComponent<UnitBase>();
            return TaskStatus.Success; // ���� ���� ���� ���� ��� Success ��ȯ
        }
        target.Value = null;
        return TaskStatus.Failure; // ���� ���� ���� ���� ��� Failure ��ȯ
    }
}
