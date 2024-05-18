using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitAgent : UnitBase
{
    public SharedGameObject target;
    public void SetTarget(SharedGameObject newTarget)
    {
        target = newTarget;
    }
    public void ApplyDamage() 
    {
        if(target.Value != null)
        {
            var targetAgent = target.Value.GetComponent<UnitAgent>();
            if(targetAgent != null)
            {
                targetAgent.OnDamaged(damage);
                Debug.Log($"적에게 {damage}만큼 피해를 입힘");
            }
        }
    }
}
