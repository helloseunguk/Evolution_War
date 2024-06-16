using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunForward : Action
{
    private Animator animator;
    public SharedGameObject target;
    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        if (target.Value != null)
        {
            Debug.Log("Å¸°Ù³×ÀÓ" + target.Name);
         
            return TaskStatus.Failure;
        }
 
        animator.SetBool("isRun", true);
        transform.Translate(Vector3.forward * Time.deltaTime * 3.0f); // Adjust speed as needed
        return TaskStatus.Running;
    }

}
