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
        // Subscribe ����
        movement.isMove
            .ObserveOnMainThread() // ���� �����忡�� ����ǵ��� ����
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
            }).AddTo(this); // MonoBehaviour�� ���� �ֱ�� �Բ� ���� ����
    }
}
