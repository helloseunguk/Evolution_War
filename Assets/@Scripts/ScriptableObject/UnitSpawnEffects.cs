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

    private ObjectPool<ParticleSystem> normalEffectPool;
    private ObjectPool<ParticleSystem> rareEffectPool;
    private ObjectPool<ParticleSystem> heroEffectPool;
    private ObjectPool<ParticleSystem> legendaryEffectPool;

    public void InitializePools(int initialSize)
    {
        normalEffectPool = new ObjectPool<ParticleSystem>(normalEffect.GetComponent<ParticleSystem>(), initialSize);
        rareEffectPool = new ObjectPool<ParticleSystem>(rareEffect.GetComponent<ParticleSystem>(), initialSize);
        heroEffectPool = new ObjectPool<ParticleSystem>(heroEffect.GetComponent<ParticleSystem>(), initialSize);
        legendaryEffectPool = new ObjectPool<ParticleSystem>(legendaryEffect.GetComponent<ParticleSystem>(), initialSize);
    }

    public ParticleSystem GetEffect(int grade)
    {
        switch (grade)
        {
            case 1: // Normal
                return normalEffectPool.Get();
            case 2: // Rare
                return rareEffectPool.Get();
            case 3: // Hero
                return heroEffectPool.Get();
            case 4: // Legendary
                return legendaryEffectPool.Get();
            default:
                return null;
        }
    }

    public void ReturnEffect(ParticleSystem effect, int grade)
    {
        switch (grade)
        {
            case 1: // Normal
                normalEffectPool.ReturnToPool(effect);
                break;
            case 2: // Rare
                rareEffectPool.ReturnToPool(effect);
                break;
            case 3: // Hero
                heroEffectPool.ReturnToPool(effect);
                break;
            case 4: // Legendary
                legendaryEffectPool.ReturnToPool(effect);
                break;
        }
    }
}
