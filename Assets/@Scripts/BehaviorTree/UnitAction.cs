using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAction : Action
{
    public SharedGameObject target;  // ���� Ÿ�� ������Ʈ
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public UnitAgent unitAgent;


    public override void OnAwake()
    {
        base.OnAwake();
        if(animator == null)
            animator = GetComponent<Animator>();
        if(navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
        if(unitAgent == null)
            unitAgent = GetComponent<UnitAgent>();
    }
}
