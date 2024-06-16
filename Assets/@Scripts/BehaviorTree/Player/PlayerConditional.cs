using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerConditional : Conditional
{
   public PlayerControl playerControl;
    public SharedGameObject target; // 타겟으로 설정할 변수
    public override void OnAwake()
    {
        base.OnAwake();
        playerControl = GetComponent<PlayerControl>();
      
    }
}
