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
            // iOS에서는 파일 접근 권한이 제한적이므로, Application.persistentDataPath 내에 파일을 위치시킵니다.
            filePath = "URI=file:" + Path.Combine(Application.persistentDataPath, "userDatabase.db");
        }
        else if (Application.isEditor)
        {
            // Unity 에디터에서는 개발 편의를 위해 persistentDataPath 또는 원하는 다른 경로를 사용할 수 있습니다.
            // 여기서는 예시로 persistentDataPath를 사용합니다.
            filePath = "URI=file:" + Path.Combine(Application.persistentDataPath, "userDatabase.db");
        }

        return filePath;
    }
    public async void DBConnectionCheck() 
    {
        Debug.Log("체크시작");
        try 
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("DB연결 성공");
            }
            else
            {
                Debug.Log("연결 실패");
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
        SaveUserDataToFirebase(userData.id, jsonData); // userData의 id를 key로 사용
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

    //    return userData; // 데이터가 없을 경우 null 반환
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
