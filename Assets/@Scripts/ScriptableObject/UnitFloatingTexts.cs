using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitFloatingTexts", menuName = "UI/UnitFloatingTexts")]
public class UnitFloatingTexts : ScriptableObject
{
    public GameObject floatingDamage;

    private ObjectPool<FloatingBase> damagePool;
    public void InitializePools(int initialSize)
    {
        damagePool = new ObjectPool<FloatingBase>(floatingDamage.GetComponent<FloatingBase>(), initialSize);
    }
    public FloatingBase GetFloatingDamage()
    {
        return damagePool.Get();
    }
    public void ReturnFloating(FloatingBase text)
    {
        damagePool.ReturnToPool(text);
    }
}
