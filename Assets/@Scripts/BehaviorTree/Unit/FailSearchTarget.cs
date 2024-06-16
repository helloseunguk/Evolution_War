using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSearchTarget : UnitAction
{
    public override void OnStart()
    {
        animator.SetBool("isIdle", true);
        navMeshAgent.isStopped = true;
    }
}
