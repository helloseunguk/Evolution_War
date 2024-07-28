using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

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
        if (EWUserInfo.userData == null)
        {
            Debug.LogError("UserData is null, not saving to file.");
            return;
        }
        EWUserInfo.RemoveDefaultUnits();


        string json = JsonConvert.SerializeObject(EWUserInfo.userData, Formatting.Indented);
        File.WriteAllText(userDataFilePath, json);
        Debug.Log("UserData saved.");
    }
    public void LoadUserData()
    {
        if (File.Exists(userDataFilePath))
        {
            string json = File.ReadAllText(userDataFilePath);
            EWUserInfo.userData = JsonConvert.DeserializeObject<UserData>(json);
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
        // �ʱ� ������ ����
        EWUserInfo.userData = new UserData
        {
            name = "Default User",
            id = "0001",
            gold = 100,
            level = 1,
            unitList = new List<UnitData>() // �ʿ信 ���� �ʱ� ���� ������ ����
        };

        // JSON���� ��ȯ �� ���Ͽ� ����
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
