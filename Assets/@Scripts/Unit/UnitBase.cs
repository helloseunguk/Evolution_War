using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UnitBase : MonoBehaviour
{
    public UnitData unitData;

    public int hp;
    public int speed;
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public bool isTeam ;
    public bool isTargeting = false;
    virtual public void Start()
    {
        hp = unitData.hp;
        speed = unitData.speed;
        damage = unitData.damage;
        attackRange = unitData.attackRange;
        attackSpeed = unitData.attackSpeed;
    }

    public void OnDamaged(int damageAmount)
    {
        hp -= damageAmount;
        if(hp <= 0)
        {
            if(isTeam)
                Managers.Battle.teamUnitList.Remove(this);
            else
                Managers.Battle.enemyUnitList.Remove(this);
            isTargeting = false;
            Destroy(gameObject);
        }
    }
}
