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
                // Ÿ���� ���� �ٷ� ȸ�� ����
                Vector3 direction = (target.Value.transform.position - player.transform.position).normalized;
                direction.y = 0; // ���� ȸ���� ����ϱ� ���� Y�� �� ����
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
