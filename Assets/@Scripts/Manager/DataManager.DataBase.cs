using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.IO;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Data;
using Mono.Data.Sqlite;
using System;


public partial class DataManager 
{

    public DatabaseReference reference;

    public async Task LocalDBCreate()
    {
        string filePath = string.Empty;
        string sourcePath = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Path.Combine(Application.persistentDataPath, "userDatabase.db");
            sourcePath = "jar:file://" + Application.dataPath + "!/assets/userDatabase.db";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            filePath = Path.Combine(Application.persistentDataPath, "userDatabase.db");
            sourcePath = Path.Combine(Application.streamingAssetsPath, "userDatabase.db");
        }
        else if (Application.isEditor)
        {
            // For Unity Editor
            filePath = Path.Combine(Application.persistentDataPath, "userDatabase.db");
            sourcePath = Path.Combine(Application.streamingAssetsPath, "userDatabase.db");
        }

        if (!File.Exists(filePath))
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(sourcePath))
            {
                await unityWebRequest.SendWebRequest();
                if (unityWebRequest.result == UnityWebRequest.Result.Success)
                {
                    File.WriteAllBytes(filePath, unityWebRequest.downloadHandler.data);
                    Debug.Log("Database created successfully at: " + filePath);
                }
                else
                {
                    Debug.LogError("Error downloading database file: " + unityWebRequest.error);
                }
            }
        }
        else
        {
            Debug.Log("Database already exists at: " + filePath);
        }
    }
    public string GetDBFilePath()
    {
        string filePath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = "URI=file:" + Path.Combine(Application.persistentDataPath, "userDatabase.db");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // iOS������ ���� ���� ������ �������̹Ƿ�, Application.persistentDataPath ���� ������ ��ġ��ŵ�ϴ�.
            filePath = "URI=file:" + Path.Combine(Application.persistentDataPath, "userDatabase.db");
        }
        else if (Application.isEditor)
        {
            // Unity �����Ϳ����� ���� ���Ǹ� ���� persistentDataPath �Ǵ� ���ϴ� �ٸ� ��θ� ����� �� �ֽ��ϴ�.
            // ���⼭�� ���÷� persistentDataPath�� ����մϴ�.
            filePath = "URI=file:" + Path.Combine(Application.persistentDataPath, "userDatabase.db");
        }

        return filePath;
    }
    public async void DBConnectionCheck() 
    {
        Debug.Log("üũ����");
        try 
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("DB���� ����");
            }
            else
            {
                Debug.Log("���� ����");
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    public void OnFirebaseSaveUserData(UserData userData)
    {
        string jsonData = JsonUtility.ToJson(userData);
        SaveUserDataToFirebase(userData.id, jsonData); // userData�� id�� key�� ���
    }
    public async Task SaveUserDataToLocal(UserData userData)
    {
        string dbPath = GetDBFilePath();
        using (IDbConnection dbConnection = new SqliteConnection(dbPath))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string query = string.Format("INSERT OR REPLACE INTO users (id, name, gold, level) VALUES ('{0}', '{1}', {2}, {3})", userData.id, userData.name, userData.gold, userData.level);
                dbCmd.CommandText = query;
                await Task.Run(() => dbCmd.ExecuteNonQuery());
            }
        }
    }

    //public async Task<UserData> LoadUserDataFromLocal()
    //{
    //    string dbPath = GetDBFilePath();
    //    UserData userData = null;

    //    using (IDbConnection dbConnection = new SqliteConnection(dbPath))
    //    {
    //        dbConnection.Open();

    //        using (IDbCommand dbCmd = dbConnection.CreateCommand())
    //        {
    //            dbCmd.CommandText = "SELECT name, gold, level FROM user";

    //            using (IDataReader reader = dbCmd.ExecuteReader())
    //            {
    //                if (reader.Read())
    //                {
    //                 //   userData = new UserData(reader.GetString(0), "localUser");
    //                    userData.gold = reader.GetInt32(1);
    //                    userData.level = reader.GetInt32(2);
    //                }
    //            }
    //        }
    //    }

    //    return userData; // �����Ͱ� ���� ��� null ��ȯ
    //}
    private void SaveUserDataToFirebase(string userId, string jsonData)
    {
        reference.Child("users").Child(userId).SetRawJsonValueAsync(jsonData).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error saving user data: " + task.Exception);
            }
            else
            {
                Debug.Log("User data saved successfully!");
            }
        });
    }
}
