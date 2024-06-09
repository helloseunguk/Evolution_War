using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitConditional : Conditional
{
    public SharedGameObject target;  // 탐지된 타겟 오브젝트
    public UnitAgent unitAgent;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public override void OnAwake()
    {
        base.OnAwake();
        Debug.Log("Start 호출");
        if(navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
        if(unitAgent == null)
        {
            unitAgent = GetComponent<UnitAgent>();
            navMeshAgent.speed = unitAgent.speed;
            navMeshAgent.ResetPath();
        }
        if(animator == null)
            animator = GetComponent<Animator>();
    }

}
