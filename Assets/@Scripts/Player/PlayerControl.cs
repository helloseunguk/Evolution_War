using UniRx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerControl : UnitAgent
{
   public PlayerAnimation animator;
   public PlayerMovement movement;
   public UnitBase targetUnit;

    // Start is called before the first frame update
    public override void Start()
    {
        isPlayable = true;
        base.Start();
        //임시용

  
        
        animator = GetComponent<PlayerAnimation>();
        movement = GetComponent<PlayerMovement>();
        areaAttack = new AreaAttack { enemyLayer = targetLayer, attackColliderType = attackColliderType }; // 광역 공격 전략 설정
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

        Vector3 attackCenter = transform.position; // 공격 중심
        areaAttack.OnAttack(attackCenter, stat);

      //  targetUnit.OnDamage();
    }
}
