using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ResetTrigger() 
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(parameter.name);
            }
        }
    }
    public void ResetBool() 
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name,false);
            }
        }
    }
    public void SetBoolAnim(Define.AnimBoolType type)
    {
        // 설정할 파라미터의 이름
        string targetParameter = type.ToString();

        // 모든 파라미터를 순회하며 설정
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                // 타겟 파라미터만 true로 설정, 나머지는 false로 설정
                animator.SetBool(parameter.name, parameter.name == targetParameter);
            }
        }
    }
    public void SetTriggerAnim(Define.AnimTriggerType type) 
    {
        string targetParameter = type.ToString();

        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if(parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.SetTrigger(parameter.name);
            }
        }
    }
}
