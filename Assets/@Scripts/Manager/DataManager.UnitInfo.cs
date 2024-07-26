using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        // Define the path to the folder containing the UnitData scriptable objects
        string folderPath = "Assets/@ScriptableObject/unit/team";

        // Get all asset paths in the specified folder
        string[] assetGUIDs = AssetDatabase.FindAssets("t:UnitData", new[] { folderPath });

        // Clear the existing list to avoid duplicates
        unitInfo.Clear();

        // Load each UnitData asset and add it to the unitList
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
        
        // If you need to perform any asynchronous operations, you can use await here
        await UniTask.CompletedTask;
    }
    public async UniTask LoadScriptEnemyInfo() 
    {
        string folderPath = "Assets/@ScriptableObject/unit/enemy";

        string[] assetGUIDs = AssetDatabase.FindAssets("t:UnitData", new[] { folderPath });

        enemyInfo.Clear();

        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath.Contains("enemy_"))
            {
                UnitData enemyData = AssetDatabase.LoadAssetAtPath<UnitData>(assetPath);
                if(enemyData !=null)
                {
                    enemyInfo.Add(enemyData);
                }
            }
        }
    }

}
