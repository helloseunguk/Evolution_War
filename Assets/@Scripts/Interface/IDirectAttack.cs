using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectAttack
{
    void OnAttack(IDamageable target, int damage);
}
