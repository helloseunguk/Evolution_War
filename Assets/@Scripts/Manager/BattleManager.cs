using System.Collections;
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

    public ReactiveCollection<UnitBase> teamUnitList = new ReactiveCollection<UnitBase>();
    public ReactiveCollection<UnitBase> enemyUnitList = new ReactiveCollection<UnitBase>();

    public void InitBattleHero(Vector3 battlePosition)
    {
        var hero = UserInfo.userHero;
        hero.transform.position = battlePosition;
    }
    public void InitBattleUnit(Vector3 battlePosition)
    {
        isArrived.Value = false;
        //생성할 범위
        const float gridWidth = 30f; 
        const float gridHeight = 20f;

        //유닛 정렬
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

        //유닛 보유 수에 맞춰 가로 세로 폭 정함
        int columns = Mathf.CeilToInt(Mathf.Sqrt(unitCount * (gridWidth / gridHeight)));
        int rows = Mathf.CeilToInt((float)unitCount / columns);

        // 유닛간의 폭
        float unitSpacingX = gridWidth / columns;
        float unitSpacingZ = gridHeight / rows;

        float startX = battlePosition.x - (gridWidth / 2) + (unitSpacingX / 2);
        float startY = battlePosition.y;
        float startZ = battlePosition.z - (gridHeight / 2) + (unitSpacingZ / 2);

        int unitIndex = 0;

        Managers.Camera.ActivateDollyCart("BattleCamera");
        //  Managers.Camera.ActivateCamera("BattleCamera");
        //   Managers.Camera.SetCameraTarget(Managers.Unit.GetUnitObject(units[0]).transform, Managers.Unit.GetUnitObject(units[0]).transform);

        foreach (var unitBase in teamUnitList)
        {
            var unitObj = unitBase.gameObject;
            if (unitObj != null)
            {
                int currentRow = unitIndex / columns;
                int currentColumn = unitIndex % columns;

                float posX = startX + (currentRow * unitSpacingX);
                float posZ = startZ + (currentColumn * unitSpacingZ);

                Vector3 targetPosition = new Vector3(posX, startY, posZ);

                unitObj.GetComponent<NavMeshAgent>().Warp(targetPosition);
                unitIndex++;
            }
        }
    }

    public void InitBattleEnemy(Vector3 battlePosition)
    {
        const float gridWidth = 30f;
        const float gridHeight = 20f;
        const float offsetDistance = 30f;

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

        Vector3 offsetPosition = new Vector3(startX - offsetDistance, startY, startZ);

        int unitIndex = 0;
        for (int i = 0; i < unitCount; i++)
        {
            int currentRow = unitIndex / columns;
            int currentColumn = unitIndex % columns;

            float posX = offsetPosition.x + (currentRow * unitSpacingX);
            float posZ = offsetPosition.z + (currentColumn * unitSpacingZ);

            Vector3 enemyPosition = new Vector3(posX, startY, posZ);

            Addressables.LoadAssetAsync<GameObject>("Enemy_01").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject enemyPrefab = handle.Result;

                    var enemyObj = GameObject.Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 90, 0));
                    var unitData = Managers.Data.GetUnitInfoScript().Find(_ => _.grade == 1 && _.level == 2);
                    var unitAgent = enemyObj.GetComponent<UnitAgent>(); // 또는 기존 컴포넌트 참조
                    unitAgent.isTeam = false;
                    unitAgent.unitData = unitData;
                    enemyUnitList.Add(unitAgent);
                }
            };

            unitIndex++;
        }
    }
}
