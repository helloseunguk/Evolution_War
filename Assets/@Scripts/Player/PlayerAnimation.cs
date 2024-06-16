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
        // ������ �Ķ������ �̸�
        string targetParameter = type.ToString();

        // ��� �Ķ���͸� ��ȸ�ϸ� ����
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                // Ÿ�� �Ķ���͸� true�� ����, �������� false�� ����
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
