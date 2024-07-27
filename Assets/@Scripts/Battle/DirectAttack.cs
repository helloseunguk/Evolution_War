using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : IDirectAttack
{
    public void OnAttack(IDamageable target, Stat stat)
    {
        target.OnDamage(stat);
    }
}
