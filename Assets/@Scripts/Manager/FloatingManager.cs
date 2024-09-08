using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingManager 
{
    public FloatingCanvas canvas;
    public UnitFloatingTexts unitFloatingText;
    private ObjectPool<TMP_Text> floatingDamage;

    public async UniTask InitSpawnFloating()
    {
        // spawnEffects ·Îµå
        if (unitFloatingText == null)
        {
            unitFloatingText = await Managers.Resource.LoadAssetAsync<UnitFloatingTexts>("UnitFloatingTexts");
            if (unitFloatingText != null)
            {
                unitFloatingText.InitializePools(10);
            }
        }
    }
    public FloatingBase OnFloatingText(Transform targetTransform, int text, bool isLoop)
    {
        var floating = unitFloatingText.GetFloatingDamage();
        floating.Init(targetTransform, (int)text, false, true);
        return floating;

    }
    public FloatingBase OnFloatingDamage(Transform targetUnit, float damage, bool isCritical)
    {
        var floating = unitFloatingText.GetFloatingDamage();
        floating.Init(targetUnit, (int)damage, isCritical);
        return floating;
    }
    public FloatingCanvas GetFloatingCanvas() 
    {
        canvas = GameObject.FindObjectOfType<FloatingCanvas>();

        return canvas; 
    }
    public void ReturnFloatingText(FloatingBase floating) 
    {
        unitFloatingText.ReturnFloating(floating);
    }
}
