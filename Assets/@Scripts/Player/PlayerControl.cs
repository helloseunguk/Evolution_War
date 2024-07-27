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
        //�ӽÿ�

  
        
        animator = GetComponent<PlayerAnimation>();
        movement = GetComponent<PlayerMovement>();
        areaAttack = new AreaAttack { enemyLayer = targetLayer, attackColliderType = attackColliderType }; // ���� ���� ���� ����
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
    public void OnAttackUnit() 
    {

        Vector3 attackCenter = transform.position; // ���� �߽�
        areaAttack.OnAttack(attackCenter, stat);

      //  targetUnit.OnDamage();
    }
}
