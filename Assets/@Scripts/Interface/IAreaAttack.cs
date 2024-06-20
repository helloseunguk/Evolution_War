using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaAttack
{
    void OnAttack(Vector3 center, int damage, float radius);
}
