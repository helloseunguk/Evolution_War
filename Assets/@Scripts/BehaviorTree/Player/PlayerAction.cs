using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Action
{
    public SharedGameObject target;
    public PlayerControl player;

    public override void OnAwake()
    {
        base.OnAwake();
        player = GetComponent<PlayerControl>();
    }
}
