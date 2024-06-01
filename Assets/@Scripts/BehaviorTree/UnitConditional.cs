using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitConditional : Conditional
{
    public SharedGameObject target;  // Ž���� Ÿ�� ������Ʈ
    public NavMeshAgent navMeshAgent;

    public override void OnStart()
    {
        base.OnStart();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

}
