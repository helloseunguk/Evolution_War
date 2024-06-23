using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class EffectManager
{
    public List<UnitBattleEffects> enemyEffectConfigs;

    private Dictionary<string, UnitBattleEffects> effectConfigMap = new Dictionary<string, UnitBattleEffects>();
    private Dictionary<string, ObjectPool<ParticleSystem>> hitEffectPools = new Dictionary<string, ObjectPool<ParticleSystem>>();
    private Dictionary<string, ObjectPool<ParticleSystem>> attackEffectPools = new Dictionary<string, ObjectPool<ParticleSystem>>();

    private Queue<(ParticleSystem, ObjectPool<ParticleSystem>, float)> effectQueue = new Queue<(ParticleSystem, ObjectPool<ParticleSystem>, float)>();

    public void Init()
    {
    }
    public void CreatePoolUnitEffects()
    {
        
    }

    public void PlayHitEffect(string enemyName, Vector3 position)
    {
        if (effectConfigMap.TryGetValue(enemyName, out var config))
        {
            if (hitEffectPools.TryGetValue(config.unitName, out var pool))
            {
                var effect = pool.Get();
                effect.transform.position = position;
                effect.transform.rotation = Quaternion.identity;
                effect.gameObject.SetActive(true);
                effect.Play();
                effectQueue.Enqueue((effect, pool, Time.time + effect.main.duration)); // 이펙트 지속 시간을 ParticleSystem의 지속 시간으로 설정
            }
        }
    }

    public void PlayAttackEffect(string enemyName, Vector3 position)
    {
        if (effectConfigMap.TryGetValue(enemyName, out var config))
        {
            if (attackEffectPools.TryGetValue(config.unitName, out var pool))
            {
                var effect = pool.Get();
                effect.transform.position = position;
                effect.transform.rotation = Quaternion.identity;
                effect.gameObject.SetActive(true);
                effect.Play();
                effectQueue.Enqueue((effect, pool, Time.time + effect.main.duration)); // 이펙트 지속 시간을 ParticleSystem의 지속 시간으로 설정
            }
        }
    }
}
