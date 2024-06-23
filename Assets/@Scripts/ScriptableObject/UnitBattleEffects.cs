using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "UnitBattleEffects", menuName = "Unit/UnitBattleEffects")]
public class UnitBattleEffects : ScriptableObject
{
    public string unitName;
    public GameObject hitEffect;
    public GameObject attackEffect;

    private ObjectPool<ParticleSystem> hitEffectPool;
    private ObjectPool<ParticleSystem> attackEffectPool;

    public void InitializePools(int initialSize)
    {
        if (hitEffect != null)
        {
            hitEffectPool = new ObjectPool<ParticleSystem>(hitEffect.GetComponent<ParticleSystem>(), initialSize);
        }
        if (attackEffect != null)
        {
            attackEffectPool = new ObjectPool<ParticleSystem>(attackEffect.GetComponent<ParticleSystem>(), initialSize);
        }
    }
    public ParticleSystem GetHitEffect()
    {
        return hitEffectPool.Get();
    }
    public ParticleSystem GetAttackEffect()
    {
        return attackEffectPool.Get();
    }
    public void ReturnMergeEffect(ParticleSystem effect)
    {
        hitEffectPool.ReturnToPool(effect);
    }
    public void ReturnSpawnEffect(ParticleSystem effect)
    {
        attackEffectPool.ReturnToPool(effect);
    }
}
