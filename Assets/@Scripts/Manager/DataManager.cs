using Cysharp.Threading.Tasks;
//using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class DataManager
{
    public async void Init()
    {
     //   reference = FirebaseDatabase.DefaultInstance.RootReference;
        userDataFilePath = Path.Combine(Application.persistentDataPath, "userData.json");

         LoadDataFromJson();

        await Managers.String.LoadStringInfo();
        await LoadScript();
          await LocalDBCreate();
        
    }
    public async UniTask LoadScript()
    {
        await LoadAllParser();
    }
}
