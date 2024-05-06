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
    public void TestUserDataSave(string userName, string userId)
    {

    }
    public void WriteNewUser(string name, string id)
    {
        UserData user = new UserData(name, id);
        string json = JsonUtility.ToJson(user);

        Debug.Log("WriteNewUser");
        string key = reference.Child("usersInfo").Push().Key;

       reference.Child("usersInfo").Child(key).SetRawJsonValueAsync(json);
    }
    public void UpdateUserName(string userId, string name)
    {
       reference.Child("usersInfo").Child(userId).Child("username").SetValueAsync(name);
    }

}
