using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class DataManager
{
    public string jsonDirectoryPath ="Assets/@Prefab/Script/unit"; // JSON 파일이 있는 디렉토리 경로

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
        foreach (var file in files)
        {
            string jsonData = File.ReadAllText(file.FullName);
            List<UnitData> units = JsonConvert.DeserializeObject<List<UnitData>>(jsonData);  // 역직렬화를 리스트로 처리
            foreach (var data in units)  // 리스트의 각 데이터에 대하여 처리
            {
                AssignDataToScriptableObject(data);
            }
        }
    }

    private void AssignDataToScriptableObject(UnitData data)
    {
        string assetPath = $"Assets/@ScriptableObject/unit_{data.grade:00}_{data.level:00}.asset";
        Debug.Log("assetPath" + assetPath);
        UnitData unitSO = Resources.Load<UnitData>(assetPath);

        if (unitSO == null)
        {
            unitSO = ScriptableObject.CreateInstance<UnitData>();
            AssetDatabase.CreateAsset(unitSO, assetPath);
        }

        unitSO.grade = data.grade;
        unitSO.level = data.level;
        unitSO.hp = data.hp;
        unitSO.damage = data.damage;
        unitSO.speed = data.speed;
        unitSO.color = data.color;
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(unitSO);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
