using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BattleManager
{
    public ReactiveProperty<bool> isArrived = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isStart = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> isDone = new ReactiveProperty<bool>(false);
    public ReactiveCollection<UnitBase> teamUnitList = new ReactiveCollection<UnitBase>();
    public ReactiveCollection<UnitBase> enemyUnitList = new ReactiveCollection<UnitBase>();
    const float offsetDistance = 75f;  // 두 팀 사이의 총 간격이 150f가 되도록 설정

    public void Init() 
    {
        isArrived.Subscribe(_ => 
        {
            if(_)
            {
                Managers.Camera.ActivateCamera("PlayerCamera");
            }
        });
    }
    public void InitBattleHero(Vector3 battlePosition)
    {
        var hero = UserInfo.userHero;
        hero.transform.position = battlePosition;
    }

    public void InitBattleUnit(Vector3 battlePosition)
    {
        const float gridWidth = 150f;
        const float gridHeight = 75f;

        var units = UserInfo.GetUnitListData();
        teamUnitList.Clear();
        foreach (var unit in units.OrderBy(_ => _.Data.grade).ThenBy(_ => _.Data.level))
        {
            var unitObj = Managers.Unit.GetUnitObject(unit);
            var unitBase = unitObj.GetComponent<UnitBase>();
            unitBase.isTeam = true;
            teamUnitList.Add(unitBase);
        }
        int unitCount = teamUnitList.Count;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(unitCount * (gridWidth / gridHeight)));
        int rows = Mathf.CeilToInt((float)unitCount / columns);

        float unitSpacingX = gridWidth / columns;
        float unitSpacingZ = gridHeight / rows;

        float startX = battlePosition.x - (gridWidth / 2) + (unitSpacingX / 2);
        float startY = battlePosition.y;
        float startZ = battlePosition.z - (gridHeight / 2) + (unitSpacingZ / 2);

        // 팀의 시작 위치를 중심에서 오른쪽으로 offsetDistance 만큼 이동
        Vector3 teamStartPosition = new Vector3(startX + offsetDistance / 2, startY, startZ);

        int unitIndex = 0;

        Managers.Camera.ActivateDollyCart("BattleCamera");

        foreach (var unitBase in teamUnitList)
        {
            var unitObj = unitBase.gameObject;
            if (unitObj != null)
            {
                int currentRow = unitIndex / columns;
                int currentColumn = unitIndex % columns;

                float posX = teamStartPosition.x + (currentRow * unitSpacingX);
                float posZ = teamStartPosition.z + (currentColumn * unitSpacingZ);

                Vector3 targetPosition = new Vector3(posX, startY, posZ);
                unitObj.transform.rotation = Quaternion.Euler(0, -90, 0);  // -90도 회전
                unitObj.GetComponent<NavMeshAgent>().Warp(targetPosition);
                unitIndex++;
            }
        }
    }

    public void InitBattleEnemy(Vector3 battlePosition)
    {
        const float gridWidth = 150f;
        const float gridHeight = 75f;

        var units = UserInfo.GetUnitListData();
        enemyUnitList.Clear();
        int unitCount = units.Count;

        int columns = Mathf.CeilToInt(Mathf.Sqrt(unitCount * (gridWidth / gridHeight)));
        int rows = Mathf.CeilToInt((float)unitCount / columns);

        float unitSpacingX = gridWidth / columns;
        float unitSpacingZ = gridHeight / rows;

        float startX = battlePosition.x - (gridWidth / 2) + (unitSpacingX / 2);
        float startY = battlePosition.y;
        float startZ = battlePosition.z - (gridHeight / 2) + (unitSpacingZ / 2);

        // 적의 시작 위치를 중심에서 왼쪽으로 offsetDistance 만큼 이동
        Vector3 enemyStartPosition = new Vector3(startX - offsetDistance / 2, startY, startZ);

        int unitIndex = 0;
        for (int i = 0; i < unitCount; i++)
        {
            int currentRow = unitIndex / columns;
            int currentColumn = unitIndex % columns;

            float posX = enemyStartPosition.x + (currentRow * unitSpacingX);
            float posZ = enemyStartPosition.z + (currentColumn * unitSpacingZ);

            Vector3 enemyPosition = new Vector3(posX, startY, posZ);

            Addressables.LoadAssetAsync<GameObject>("Enemy_01").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject enemyPrefab = handle.Result;

                    var enemyObj = GameObject.Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 90, 0));
                    var unitData = Managers.Data.GetUnitInfoScript().Find(_ => _.grade == 1 && _.level == 2);
                    var unitAgent = enemyObj.GetComponent<UnitAgent>();
                    unitAgent.isTeam = false;
                    unitAgent.unitData = unitData;
                    enemyUnitList.Add(unitAgent);
                }
            };

            unitIndex++;
        }
    }
}
