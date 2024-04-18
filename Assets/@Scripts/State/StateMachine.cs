using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    //현재 상태
    public IState CurrentState { get; private set; }

    //생성자에서 기본 상태 설정
    public StateMachine(IState defaultState)
    {
        CurrentState = defaultState;
    }

    //현재 상태 설정
    public void SetState(IState state)
    {
        // 예외처리.
        if (CurrentState == state)
        {
            return;
        }

        //상태가 바뀌기 직전 호출
        CurrentState.OperateExit();

        //상태 교체
        CurrentState = state;

        //생태가 바뀌면 호출
        CurrentState.OperateEnter();
    }

    //매 프레임 호출
    public void DoOperateUpdate()
    {
        CurrentState.OperateUpdate();
    }
}
