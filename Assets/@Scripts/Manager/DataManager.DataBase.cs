using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public partial class DataManager 
{

    public DatabaseReference reference;

    public void Init()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnSaveUserData(UserData userData)
    {
        string jsonData = JsonUtility.ToJson(userData);
        SaveUserDataToFirebase(userData.id, jsonData); // userData의 id를 key로 사용
    }

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
