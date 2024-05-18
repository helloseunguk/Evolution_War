using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UnitBase : MonoBehaviour
{
    public UnitData unitData;

    public int hp;
    public int speed;
    public int damage;

    virtual public void Start()
    {
        hp = unitData.hp;
        speed = unitData.speed;
        damage = unitData.damage;
    }

    public void OnDamaged(int damageAmount)
    {
        hp -= damageAmount;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
