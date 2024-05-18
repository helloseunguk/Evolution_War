using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTargetInRange : Conditional
{
    public string targetTag;  // Ÿ���� �±�
    public float range;       // Ž�� ����
    public SharedGameObject target;  // Ž���� Ÿ�� ������Ʈ
    public float checkInterval = 1.0f;  // Ž�� �ֱ� (�� ����)
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
                    target.Value = collider.gameObject;  // Ÿ�� ����
                    GetComponent<UnitAgent>().SetTarget(target.Value);
                    return TaskStatus.Success;  // ���� ����
                }
            }
        }

        return TaskStatus.Failure;  // ���� ����
    }
}
