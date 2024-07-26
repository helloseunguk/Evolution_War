using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager
{
    public async UniTask LoadAllParser() 
    {
        await UniTask.WhenAll(
                    LoadScriptUnitInfo(),
                    LoadScriptEnemyInfo(),
                    LoadScriptStageInfo()
            );
       
    }
}
