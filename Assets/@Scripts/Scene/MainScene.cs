using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MainScene : BaseScene
{
    // Start is called before the first frame update
   public GameObject lobbyUI;
   public GameObject battleUI;
    public Transform spawnTransform;

    public override void Start()
    {
        base.Start();
        Init();

        Managers.Battle.isStart.ObserveEveryValueChanged(_ =>_.Value).Subscribe(_ => 
        {
            if(_)
            {
                lobbyUI.SetActive(false);
            }
        });
        Managers.Battle.isArrived.ObserveEveryValueChanged(_ => _.Value).Subscribe(_ => 
        {
            if(_)
            {
                battleUI.SetActive(true);
            }
        });
    }
    private async UniTask Init()
    {
       await Managers.Spawn.InitSpawnEffect();
       await Managers.Floating.InitSpawnFloating();

        Managers.Camera.RegistAllCamera();
        Managers.Camera.ActivateCamera("LobbyCamera");

        OnUnitSpawn();
    }
    private void OnUnitSpawn() 
    {
        List<UnitData> unitsToRemove = new List<UnitData>();

        foreach (var unit in EVUserInfo.userData.unitList)
        {
            // 유닛을 스폰
            Managers.Spawn.SpawnUnit(spawnTransform.position, true,false, spawnTransform,unit);
            // 제거할 유닛을 임시 리스트에 추가
            unitsToRemove.Add(unit);
        }

        // 모든 유닛을 스폰한 후, 임시 리스트에서 unitList에서 제거
        foreach (var unit in unitsToRemove)
        {
            EVUserInfo.userData.unitList.Remove(unit);
        }
    }
}
