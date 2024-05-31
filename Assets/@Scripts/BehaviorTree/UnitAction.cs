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

    public override void OnAwake()
    {
      animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
