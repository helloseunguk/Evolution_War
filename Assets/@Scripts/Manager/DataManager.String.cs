using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class StringScript 
{
    public int stringID;
    public Define.SystemLanguage languageType;
    public string stringData;
}
public partial class DataManager 
{
     private List<StringScript> listStringScript = new List<StringScript>();

    private List<StringScript> GetStringScript
    {
        get { return listStringScript; }
    }

    [Serializable]
    public class StringScriptAll
    {
        public StringScript[] result;
    }
    void ClearString()
    {
        listStringScript.Clear();
    }
    async UniTask LoadScriptString()
    {
        var load = await Managers.Resource.LoadScript("stringKorean");
        if(load == "")
        {
            Debug.LogWarning("String is empty");
            return;
        }

        var resultScript = JsonUtility.FromJson<StringScriptAll>("{ \"result\" : " + load + "}");
        for (int i = 0; i < resultScript.result.Length; i++)
            listStringScript.Add(resultScript.result[i]);
    }
}
