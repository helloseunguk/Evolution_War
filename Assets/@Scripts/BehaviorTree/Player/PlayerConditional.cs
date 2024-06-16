using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerConditional : Conditional
{
   public PlayerControl playerControl;
    public SharedGameObject target; // Ÿ������ ������ ����
    public override void OnAwake()
    {
        base.OnAwake();
        playerControl = GetComponent<PlayerControl>();
      
    }
}
