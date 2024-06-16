using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class OnPlayerTargetAttack : PlayerAction
{
    public float attackInterval = 1.0f;
    private float nextAttackTime = 0.0f;

    public override TaskStatus OnUpdate()
    {
        if (player.movement.isMove.Value == true)
        {
            player.animator.ResetTrigger();
            return TaskStatus.Failure;
        }
        if (target.Value == null)
        {
            player.animator.ResetTrigger();
            return TaskStatus.Failure;
        }
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackInterval;

            UnitAgent targetAgent = target.Value.GetComponent<UnitAgent>();
            if (targetAgent != null)
            {
                // 타겟을 향해 바로 회전 설정
                Vector3 direction = (target.Value.transform.position - player.transform.position).normalized;
                direction.y = 0; // 수평 회전만 고려하기 위해 Y축 값 무시
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    player.transform.rotation = lookRotation;
                }

                player.animator.ResetBool();
                // targetAgent.OnDamaged(GetComponent<UnitAgent>().damage);
                player.animator.SetTriggerAnim(Define.AnimTriggerType.onAttack);
            }

            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
