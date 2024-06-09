using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class DataManager
{
    UnitSpawnProbability spawnProbability;
    public UnitSpawnProbability GetUnitSpawnProbability()
    {
        return spawnProbability;
    }
    //public async UniTask LoadScriptUnitSpawnProbabilityInfo()
    //{
    //    // Define the path to the folder containing the UnitData scriptable objects
    //    string folderPath = "Assets/@ScriptableObject/probability";

    //    // Get all asset paths in the specified folder
    //    string[] assetGUIDs = AssetDatabase.FindAssets("t:UnitData", new[] { folderPath });

    //    // Clear the existing list to avoid duplicates
    //    unitInfo.Clear();

    //    // Load each UnitData asset and add it to the unitList
    //    foreach (string guid in assetGUIDs)
    //    {
    //        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
    //        if (assetPath.Contains("unit_"))
    //        {
    //            UnitData unitData = AssetDatabase.LoadAssetAtPath<UnitData>(assetPath);
    //            if (unitData != null)
    //            {
    //                unitInfo.Add(unitData);
    //            }
    //        }
    //    }

    //    // If you need to perform any asynchronous operations, you can use await here
    //    await UniTask.CompletedTask;
    //}
}
