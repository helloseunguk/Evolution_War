using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    //���� ����
    public IState CurrentState { get; private set; }

    //�����ڿ��� �⺻ ���� ����
    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
    }

    //���� ���� ����
    public void SetState(IState state)
    {
        // ����ó��.
        if (CurrentState == state)
        {
            return;
        }

        //���°� �ٲ�� ���� ȣ��
        CurrentState.OperateExit();

        //���� ��ü
        CurrentState = state;

        //���°� �ٲ�� ȣ��
        CurrentState.OperateEnter();
    }

    //�� ������ ȣ��
    public void DoOperateUpdate()
    {
        CurrentState.OperateUpdate();
    }
}
