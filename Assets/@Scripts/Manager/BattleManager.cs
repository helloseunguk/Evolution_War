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
    public ReactiveProperty<bool> isArrived = new ReactiveProperty<bool>();
    public void InitBattleHero(Vector3 battlePosition)
    {
        var hero = UserInfo.userHero;
        hero.transform.position = battlePosition;
    }
    public void InitBattleUnit(Vector3 battlePosition)
    {
        isArrived.Value = false;
        //»ý¼ºÇÒ ¹üÀ§
        const float gridWidth = 30f; 
        const float gridHeight = 20f; 

        //À¯´Ö Á¤·Ä
        var units = UserInfo.GetUnitListData();
        units = units.OrderBy(_ => _.Data.grade).ThenBy(_ => _.Data.level).ToList();
        int unitCount = units.Count;

        //À¯´Ö º¸À¯ ¼ö¿¡ ¸ÂÃç °¡·Î ¼¼·Î Æø Á¤ÇÔ
        int columns = Mathf.CeilToInt(Mathf.Sqrt(unitCount * (gridWidth / gridHeight)));
        int rows = Mathf.CeilToInt((float)unitCount / columns);

        // À¯´Ö°£ÀÇ Æø
        float unitSpacingX = gridWidth / columns;
        float unitSpacingZ = gridHeight / rows;

        float startX = battlePosition.x - (gridWidth / 2) + (unitSpacingX / 2);
        float startY = battlePosition.y;
        float startZ = battlePosition.z - (gridHeight / 2) + (unitSpacingZ / 2);

        int unitIndex = 0;

        Managers.Camera.ActivateDollyCart("BattleCamera");
      //  Managers.Camera.ActivateCamera("BattleCamera");
     //   Managers.Camera.SetCameraTarget(Managers.Unit.GetUnitObject(units[0]).transform, Managers.Unit.GetUnitObject(units[0]).transform);
       
        foreach (var unit in units)
        {
            var unitObj = Managers.Unit.GetUnitObject(unit);
            if (unitObj != null)
            {
                int currentRow = unitIndex / columns;
                int currentColumn = unitIndex % columns;

                // Swap X and Z positions to rotate the grid by 90 degrees
                float posX = startX + (currentRow * unitSpacingX);
                float posZ = startZ + (currentColumn * unitSpacingZ);

                Vector3 targetPosition = new Vector3(posX, startY, posZ);


                //   unitObj.transform.position = targetPosition;
                unitObj.GetComponent<NavMeshAgent>().Warp(targetPosition);
                // Reset the unit's rotation
             //   unitObj.transform.rotation = Quaternion.Euler(0, -90, 0);

                unitIndex++;
            }
        }
    }

    public void InitBattleEnemy(Vector3 battlePosition)
    {
        const float gridWidth = 30f;  // Width of the area
        const float gridHeight = 20f; // Height of the area
        const float offsetDistance = 30f;  // Distance to place enemies from the units

        // Assuming you want to spawn the same number of enemies as units
        var units = UserInfo.GetUnitListData();
        int unitCount = units.Count;

        // Calculate columns and rows based on grid size
        int columns = Mathf.CeilToInt(Mathf.Sqrt(unitCount * (gridWidth / gridHeight)));
        int rows = Mathf.CeilToInt((float)unitCount / columns);

        // Calculate unit spacing
        float unitSpacingX = gridWidth / columns;
        float unitSpacingZ = gridHeight / rows;

        // Starting positions
        float startX = battlePosition.x - (gridWidth / 2) + (unitSpacingX / 2);
        float startY = battlePosition.y;
        float startZ = battlePosition.z - (gridHeight / 2) + (unitSpacingZ / 2);

        // Calculate the offset position based on the facing direction of the units
        Vector3 offsetPosition = new Vector3(startX - offsetDistance, startY, startZ);

        int unitIndex = 0;
        for (int i = 0; i < unitCount; i++)
        {
            int currentRow = unitIndex / columns;
            int currentColumn = unitIndex % columns;

            // Swap X and Z positions to rotate the grid by 90 degrees
            float posX = offsetPosition.x + (currentRow * unitSpacingX);
            float posZ = offsetPosition.z + (currentColumn * unitSpacingZ);

            Vector3 enemyPosition = new Vector3(posX, startY, posZ);

            // Load the enemy prefab from Addressables
            Addressables.LoadAssetAsync<GameObject>("Enemy_01").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject enemyPrefab = handle.Result;
                    
                    var enemyObj = GameObject.Instantiate(enemyPrefab, enemyPosition, Quaternion.Euler(0, 90, 0));
                    enemyObj.GetComponent<UnitAgent>().unitData = Managers.Data.GetUnitInfoScript().Find(_ => _.grade == 1 && _.level == 2);
                    // Add additional setup for the enemy if necessary
                }
            };

            unitIndex++;
        }
    }
}
