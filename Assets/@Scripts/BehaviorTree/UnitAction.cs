using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAction : Action
{
    public SharedGameObject target;  // 따라갈 타겟 오브젝트
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    public override void OnAwake()
    {
      animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
