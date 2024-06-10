using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSpawnEffects", menuName = "Unit/SpawnEffects")]
public class UnitSpawnEffects : ScriptableObject
{
    public GameObject normalEffect;
    public GameObject rareEffect;
    public GameObject heroEffect;
    public GameObject legendaryEffect;
    public GameObject mergeEffect;

    private ObjectPool<ParticleSystem> normalEffectPool;
    private ObjectPool<ParticleSystem> rareEffectPool;
    private ObjectPool<ParticleSystem> heroEffectPool;
    private ObjectPool<ParticleSystem> legendaryEffectPool;
    private ObjectPool<ParticleSystem> mergeEffectPool;
    public void InitializePools(int initialSize)
    {
        normalEffectPool = new ObjectPool<ParticleSystem>(normalEffect.GetComponent<ParticleSystem>(), initialSize);
        rareEffectPool = new ObjectPool<ParticleSystem>(rareEffect.GetComponent<ParticleSystem>(), initialSize);
        heroEffectPool = new ObjectPool<ParticleSystem>(heroEffect.GetComponent<ParticleSystem>(), initialSize);
        legendaryEffectPool = new ObjectPool<ParticleSystem>(legendaryEffect.GetComponent<ParticleSystem>(), initialSize);
        mergeEffectPool = new ObjectPool<ParticleSystem>(mergeEffect.GetComponent<ParticleSystem>(), initialSize);
    }
    public ParticleSystem GetMergeEffect()
    {
        return mergeEffectPool.Get();
    }
    public ParticleSystem GetSpawnEffect(Define.SpawnRarity spawnRarity)
    {
        switch (spawnRarity)
        {
            case Define.SpawnRarity.Normal: // Normal
                return normalEffectPool.Get();
            case Define.SpawnRarity.Rare: // Rare
                return rareEffectPool.Get();
            case Define.SpawnRarity.Hero: // Hero
                return heroEffectPool.Get();
            case Define.SpawnRarity.Legendary: // Legendary
                return legendaryEffectPool.Get();
            default:
                return null;
        }
    }
    public void ReturnMergeEffect(ParticleSystem effect)
    {
        mergeEffectPool.ReturnToPool(effect);
    }
    public void ReturnSpawnEffect(ParticleSystem effect,Define.SpawnRarity spawnRarity)
    {
        switch (spawnRarity)
        {
            case Define.SpawnRarity.Normal: // Normal
                normalEffectPool.ReturnToPool(effect);
                break;
            case Define.SpawnRarity.Rare: // Rare
                rareEffectPool.ReturnToPool(effect);
                break;
            case Define.SpawnRarity.Hero: // Hero
                heroEffectPool.ReturnToPool(effect);
                break;
            case Define.SpawnRarity.Legendary: // Legendary
                legendaryEffectPool.ReturnToPool(effect);
                break;
        }
    }
}
