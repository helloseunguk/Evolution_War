using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class StageInfoScript
{
    public int Stage;
    public int Level;
    public int CloseUnitCount;
    public int LongUnitCount;
    public int MagicUnitCount;
    public int RewardGold;
    public int RewardGem;
}

public partial class DataManager
{
    private List<StageInfoScript> listStageInfoScript = null;

    public StageInfoScript GetStageInfoScript(Predicate<StageInfoScript> predicate)
    {
        return listStageInfoScript.Find(predicate);
    }

    public List<StageInfoScript> GetStageInfoScriptList
    {
        get
        {
            return listStageInfoScript;
        }
    }

    void ClearStageInfo()
    {
        listStageInfoScript?.Clear();
    }

    public async UniTask LoadScriptStageInfo()
    {
        List<StageInfoScript> resultScript = null;

        var load = await Managers.Resource.LoadScript("stageInfo");

        if (string.IsNullOrEmpty(load))
        {
            Debug.LogWarning("StageInfo is Empty");
            return;
        }

        try
        {
            resultScript = JsonConvert.DeserializeObject<List<StageInfoScript>>(load);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}");
            return;
        }

        listStageInfoScript = resultScript;
    }
}
