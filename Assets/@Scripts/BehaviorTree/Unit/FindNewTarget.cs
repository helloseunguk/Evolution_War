using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNewTarget : Action
{
    public float largeDetectionRadius = 20f;
    public SharedTransform target;

    public override TaskStatus OnUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, largeDetectionRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Unit"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            target.Value = closestTarget;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
