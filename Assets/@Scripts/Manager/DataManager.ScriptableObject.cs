using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class DataManager
{
    public string jsonDirectoryPath = "Assets/@Prefab/scripts/unit"; // JSON 파일이 있는 디렉토리 경로

    private void LoadDataFromJson()
    {
        string fullPath = Path.Combine(jsonDirectoryPath);
        DirectoryInfo dir = new DirectoryInfo(fullPath);
        if (!dir.Exists)
        {
            Debug.LogError("Directory does not exist: " + fullPath);
            return;
        }

        FileInfo[] files = dir.GetFiles("*.json");
        Debug.Log($"Found {files.Length} JSON files.");

        foreach (var file in files)
        {
            string jsonData = File.ReadAllText(file.FullName);
            List<UnitData> units = JsonConvert.DeserializeObject<List<UnitData>>(jsonData);  // 역직렬화를 리스트로 처리

            Debug.Log($"Processing file: {file.Name}, Number of units: {units.Count}");

            foreach (var data in units)  // 리스트의 각 데이터에 대하여 처리
            {
                AssignDataToScriptableObject(data, file.Name);
            }
        }
    }

    private void AssignDataToScriptableObject(UnitData data, string fileName)
    {
        string assetPath = "";

        if (fileName.Contains("unitInfo"))
        {
            assetPath = $"Assets/@ScriptableObject/unit/team/unit_{data.grade:00}_{data.level:00}_{data.attackType}.asset";
        }
        else if (fileName.Contains("enemyInfo"))
        {
            assetPath = $"Assets/@ScriptableObject/unit/enemy/enemy_{data.grade:00}_{data.level:00}_{data.attackType}.asset";
        }

#if UNITY_EDITOR
        UnitData unitSO = AssetDatabase.LoadAssetAtPath<UnitData>(assetPath);

        if (unitSO == null)
        {
            unitSO = ScriptableObject.CreateInstance<UnitData>();
            AssetDatabase.CreateAsset(unitSO, assetPath);
        }

        unitSO.grade = data.grade;
        unitSO.level = data.level;
        unitSO.attackType = data.attackType;
        unitSO.color = data.color;
        unitSO.hp = data.hp;
        unitSO.damage = data.damage;
        unitSO.speed = data.speed;
        unitSO.attackRange = data.attackRange;
        unitSO.attackSpeed = data.attackSpeed;
        unitSO.attackRadius = data.attackRadius;
        unitSO.goldIncome = data.goldIncome;
        EditorUtility.SetDirty(unitSO);
        AssetDatabase.SaveAssets();
#endif
    }
}
