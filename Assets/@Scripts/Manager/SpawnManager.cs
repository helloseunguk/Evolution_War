using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnManager 
{
    public UnitSpawnProbability spawnProbability;
    public UnitSpawnEffects spawnEffects;

    public void Init()
    {
        Addressables.LoadAssetAsync<UnitSpawnProbability>("UnitSpawnProbability").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("확률 할당");
                spawnProbability = handle.Result;
            }
        };
        Addressables.LoadAssetAsync<UnitSpawnEffects>("UnitSpawnEffects").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("이펙트 할당");
                spawnEffects = handle.Result;
                spawnEffects.InitializePools(10);
            }
        };
    }
    public void SpawnUnit(UnitData _unit, Vector3 spawnPosition, bool isRandomPosition, Transform parent = null)
    {
        if (_unit == null)
            return;
        if (isRandomPosition)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 10;  // 반경 10 이내의 랜덤 벡터 생성
            spawnPosition = new Vector3(spawnPosition.x + randomOffset.x,
                                                spawnPosition.y,
                                                spawnPosition.z + randomOffset.y);
        }

        Addressables.LoadAssetAsync<GameObject>($"unit_{_unit.grade}").Completed += handle =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject unitPrefab = handle.Result;

                unitPrefab.GetComponent<UnitAgent>().unitData = _unit;
                var obj = GameObject.Instantiate(unitPrefab, spawnPosition, Quaternion.identity, parent); // 부모 Transform을 설정합니다.
                Color colorValue;
                if (UnityEngine.ColorUtility.TryParseHtmlString(_unit.color, out colorValue))
                {
                    Renderer renderer = obj.GetComponentInChildren<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = colorValue;
                    }
                }
                var unit = new Unit(_unit);
      
                PlaySpawnEffect(4, spawnPosition);
                UserInfo.AddUnitData(unit);
                Managers.Unit.RegisterGameObject(unit, obj);
            }
        };
    }

    public void MergeUnit(Transform parent = null)
    {
        // Get the list of units the user currently owns
        var units = UserInfo.GetUnitListData();
        if (units.Count < 2)
        {
            return;
        }

        // Sort units by grade and level
        var sortUnits = units.OrderBy(_ => _.Data.grade).ThenBy(_ => _.Data.level).ToList();

        // Iterate through the sorted list to find the first pair of units that can be merged
        for (int i = 0; i < sortUnits.Count - 1; i++)
        {
            var unit1 = sortUnits[i];
            var unit2 = sortUnits[i + 1];

            if (unit1.Data.level == unit2.Data.level && unit1.Data.grade == unit2.Data.grade)
            {
                var unitObj1 = Managers.Unit.GetUnitObject(unit1);
                var unitObj2 = Managers.Unit.GetUnitObject(unit2);

                if (unitObj1 == null || unitObj2 == null)
                {
                    continue;
                }

                // Calculate the midpoint position for interpolation
                Vector3 midPosition = Vector3.Lerp(unitObj1.transform.position, unitObj2.transform.position, 0.5f);

                // Move the first unit to the midpoint and disable it
                unitObj1.transform.DOMove(midPosition, 0.5f).OnComplete(() =>
                {
                    unitObj1.SetActive(false);

                    // Get the new unit data based on the merged unit's level and grade
                    var unitList = Managers.Data.GetUnitInfoScript();
                    int nextLevel = (unit1.Data.grade - 1) * 5 + unit1.Data.level + 1;
                    int unitGrade = (nextLevel - 1) / 5 + 1; // Increase grade every 5 levels
                    int unitLevel = (nextLevel - 1) % 5 + 1; // Level cycles from 1 to 5

                    var unitData = unitList.Find(_ => _.level == unitLevel && _.grade == unitGrade);

                    // Spawn the new unit at the midpoint position
                    SpawnUnit(unitData, midPosition, false, parent);
                });

                // Move the second unit to the midpoint and disable it
                unitObj2.transform.DOMove(midPosition, 0.5f).OnComplete(() =>
                {
                    unitObj2.SetActive(false);
                });

                // Remove the merged units from the list
                units.Remove(unit1);
                UserInfo.RemoveUnitData(unit1);
                units.Remove(unit2);
                UserInfo.RemoveUnitData(unit2);
                break; // Exit the loop after merging a pair
            }
        }
    }
    private void PlaySpawnEffect(int grade, Vector3 position)
    {
        var effect = spawnEffects.GetEffect(grade);
        if (effect != null)
        {
            effect.transform.position = position;
            effect.Play();
            DOVirtual.DelayedCall(effect.main.duration + effect.main.startLifetime.constantMax, () =>
            {
                effect.Stop();
                spawnEffects.ReturnEffect(effect, grade);
            });
        }
    }
}
