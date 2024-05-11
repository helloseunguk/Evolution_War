using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public partial class DataManager
{
    private string userDataFilePath;


    public void OnApplicationQuit()
    {
        Debug.Log("QuitApplication");
        SaveUserData();
    }
    public void SaveUserData()
    {
        if (UserInfo.userData == null)
        {
            Debug.LogError("UserData is null, not saving to file.");
            return;
        }

        string json = JsonConvert.SerializeObject(UserInfo.userData, Formatting.Indented);
        File.WriteAllText(userDataFilePath, json);
        Debug.Log("UserData saved.");
    }
    public void LoadUserData()
    {
        if (File.Exists(userDataFilePath))
        {
            string json = File.ReadAllText(userDataFilePath);
            UserInfo.userData = JsonConvert.DeserializeObject<UserData>(json);
            Debug.Log("UserData loaded.");
        }
        else
        {
            Debug.Log("No UserData file found, creating new data.");
            CreateNewJson();
        }
    }
    private void CreateNewJson()
    {
        // 초기 데이터 설정
        UserInfo.userData = new UserData
        {
            name = "Default User",
            id = "0001",
            gold = 100,
            level = 1,
            unitList = new List<UnitData>() // 필요에 따라 초기 유닛 데이터 설정
        };

        // JSON으로 변환 후 파일에 저장
        SaveUserData();
    }
    public void DeleteUserDataFile()
    {
        if (File.Exists(userDataFilePath))
        {
            File.Delete(userDataFilePath);
            Debug.Log("UserData file deleted.");
        }
        else
        {
            Debug.Log("No UserData file found to delete.");
        }
    }
}
