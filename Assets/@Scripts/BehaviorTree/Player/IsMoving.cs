using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoving : PlayerConditional
{
    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
            return TaskStatus.Success;

        if (playerControl.movement.isMove.Value)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
