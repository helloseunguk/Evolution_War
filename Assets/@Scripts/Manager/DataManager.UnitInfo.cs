using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class DataManager
{
    public List<UnitData> unitInfo = new List<UnitData>();
    public List<UnitData> enemyInfo = new List<UnitData>();

    public List<UnitData> GetUnitInfoScript()
    {
        return unitInfo;
    }

    public List<UnitData> GetEnemyInfoScript()
    {
        return enemyInfo;
    }

    public async UniTask LoadScriptUnitInfo()
    {
        unitInfo.Clear();

#if UNITY_EDITOR
        // Editor: Use AssetDatabase to load assets
        string folderPath = "Assets/@ScriptableObject/unit/team";
        string[] assetGUIDs = AssetDatabase.FindAssets("t:UnitData", new[] { folderPath });

        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath.Contains("unit_"))
            {
                UnitData unitData = AssetDatabase.LoadAssetAtPath<UnitData>(assetPath);
                if (unitData != null)
                {
                    unitInfo.Add(unitData);
                }
            }
        }
#else
        // Build and Editor (Addressables): Load assets using Addressables
        AsyncOperationHandle<IList<UnitData>> handle = Addressables.LoadAssetsAsync<UnitData>(
            "unit/team", // This should be a label assigned to the assets
            unitData =>
            {
                if (unitData.name.Contains("unit_"))
                {
                    unitInfo.Add(unitData);
                }
            });

        await handle.Task;
        
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to load UnitData from Addressables.");
        }
#endif
        await UniTask.CompletedTask;
    }

    public async UniTask LoadScriptEnemyInfo()
    {
        enemyInfo.Clear();

#if UNITY_EDITOR
        // Editor: Use AssetDatabase to load assets
        string folderPath = "Assets/@ScriptableObject/unit/enemy";
        string[] assetGUIDs = AssetDatabase.FindAssets("t:UnitData", new[] { folderPath });

        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath.Contains("enemy_"))
            {
                UnitData enemyData = AssetDatabase.LoadAssetAtPath<UnitData>(assetPath);
                if (enemyData != null)
                {
                    enemyInfo.Add(enemyData);
                }
            }
        }
#else
        // Build and Editor (Addressables): Load assets using Addressables
        AsyncOperationHandle<IList<UnitData>> handle = Addressables.LoadAssetsAsync<UnitData>(
            "unit/enemy", // This should be a label assigned to the assets
            enemyData =>
            {
                if (enemyData.name.Contains("enemy_"))
                {
                    enemyInfo.Add(enemyData);
                }
            });

        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to load EnemyData from Addressables.");
        }
#endif
        await UniTask.CompletedTask;
    }
}
