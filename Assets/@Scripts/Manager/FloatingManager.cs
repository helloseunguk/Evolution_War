using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingManager 
{
    public FloatingCanvas canvas;
    public UnitFloatingTexts floatingText;

    private ObjectPool<TMP_Text> floatingDamage;

    public async UniTask InitSpawnFloating()
    {
        // spawnEffects ·Îµå
        if (floatingText == null)
        {
            floatingText = await Managers.Resource.LoadAssetAsync<UnitFloatingTexts>("UnitFloatingTexts");
            if (floatingText != null)
            {
                floatingText.InitializePools(10);
            }
        }
    }
    public void OnFloatingDamage(Transform targetUnit, int damage)
    {
        var floating = floatingText.GetFloatingDamage();
        floating.Init(targetUnit, damage);
    }
    public FloatingCanvas GetFloatingCanvas() 
    {
        canvas = GameObject.FindObjectOfType<FloatingCanvas>();

        return canvas; 
    }
    public void ReturnFloatingText(FloatingBase floating) 
    {
        floatingText.ReturnFloating(floating);
    }
}
