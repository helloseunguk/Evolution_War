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
            // ������ ����
            Managers.Spawn.SpawnUnit(spawnTransform.position, true,false, spawnTransform,unit);
            // ������ ������ �ӽ� ����Ʈ�� �߰�
            unitsToRemove.Add(unit);
        }

        // ��� ������ ������ ��, �ӽ� ����Ʈ���� unitList���� ����
        foreach (var unit in unitsToRemove)
        {
            EVUserInfo.userData.unitList.Remove(unit);
        }
    }
}
