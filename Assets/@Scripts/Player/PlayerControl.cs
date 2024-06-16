using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerControl : MonoBehaviour
{
   public PlayerAnimation animator;
   public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<PlayerAnimation>();
        movement = GetComponent<PlayerMovement>();
        movement.isMove.Value = false;
        // Subscribe 설정
        movement.isMove
            .ObserveOnMainThread() // 메인 스레드에서 실행되도록 보장
            .Subscribe(_ =>
            {
                if (_)
                {
                    animator.SetBoolAnim(Define.AnimBoolType.isMove);
                }
                else
                {
                    animator.SetBoolAnim(Define.AnimBoolType.isIdle);
                }
            }).AddTo(this); // MonoBehaviour의 생명 주기와 함께 구독 관리
    }
}
