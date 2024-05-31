using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class IsStartWait : UnitConditional
{
    public override TaskStatus OnUpdate()
    {
        if(Managers.Battle.isArrived.Value == true)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
