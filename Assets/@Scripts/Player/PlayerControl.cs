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
   public UnitBase targetUnit;
    PlayerStat stat;

    public void Init(PlayerStat _stat) 
    {
        stat = _stat;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //임시용
        stat = new PlayerStat();
        stat.damage = 10;

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
    public void OnAttackUnit() 
    {

        targetUnit.OnDamage(stat.damage);
        Managers.Floating.OnFloatingDamage(targetUnit.transform, stat.damage);
      //  targetUnit.OnDamage();
    }
}
