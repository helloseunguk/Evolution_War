using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitConditional : Conditional
{
    public SharedGameObject target;  // Å½ÁöµÈ Å¸°Ù ¿ÀºêÁ§Æ®
    public UnitAgent unitAgent;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public override void OnAwake()
    {
        base.OnAwake();
        if(navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
        if(unitAgent == null)
        {
            unitAgent = GetComponent<UnitAgent>();
            navMeshAgent.speed = unitAgent.stat.moveSpeed;
            navMeshAgent.ResetPath();
        }
        if(animator == null)
            animator = GetComponent<Animator>();
    }

}
