using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AddressableAssets;

public class SpawnManager 
{
    UnitData unitData;

    public void SpawnUnit(UnitData _unit,Vector3 spawnPosition,bool isRandomPosition) 
    {
        if (_unit == null)
            return;

        unitData = _unit;
        if (isRandomPosition)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 10;  // �ݰ� 10 �̳��� ���� ���� ����
            spawnPosition = new Vector3(spawnPosition.x + randomOffset.x,
                                                spawnPosition.y,
                                                spawnPosition.z + randomOffset.y);
        }

       

        Addressables.LoadAssetAsync<GameObject>($"unit_{_unit.grade}").Completed += handle =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject unitPrefab = handle.Result;
                unitPrefab.GetComponent<UnitAgent>().unitData = unitData;
                var obj = GameObject.Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
                Color colorValue;
                if(UnityEngine.ColorUtility.TryParseHtmlString(_unit.color,out colorValue))
                {
                    Renderer renderer = obj.GetComponentInChildren<Renderer>();
                    if(renderer != null)
                    {
                        renderer.material.color = colorValue;
                    }
                }
                var unit = new Unit(_unit);
                UserInfo.AddUnitData(unit);
                Managers.Unit.RegisterGameObject(unit, obj);
            }
        };
    }
    public void MergeUnit() 
    {
        //UserInfo.GetUnitList()�� ���� ������ ������ ���� ����Ʈ�� �޾ƿ´�
        var units = UserInfo.GetUnitListData();
        if (units.Count <2)
        {
            return;
        }
        //�ش� ����Ʈ���� ���� ���� �� ����� ���� ���� 2���� ã�� ������ ���ٸ� �� ���� ������ Position�� ã�´�
        var sortUnits = units.OrderBy(_ => _.Data.level).ThenBy(_ => _.Data.grade).ToList();

        var unit1 = sortUnits[0];
        var unit2 = sortUnits[1];

        if (unit1.Data.level != unit2.Data.level)
            return;
        if (unit1.Data.grade != unit2.Data.grade)
            return;

        var unitObj1 = Managers.Unit.GetUnitObject(unit1);
        var unitObj2 = Managers.Unit.GetUnitObject(unit2);

        if (unitObj1 == null || unitObj2 == null)
        {
            return;
        }
        //Dotween���� �� ������ġ�� ����ó���ϰ� Position���� 1���� �������� ��Ȱ��ȭ
        Vector3 midPosition = Vector3.Lerp(unitObj1.transform.position, unitObj2.transform.position, 0.5f);
        unitObj1.transform.DOMove(midPosition, 1.0f).OnComplete(() => 
        {
            unitObj1.SetActive(false);
            SpawnUnit(unitData, midPosition,false);
        });
        unitObj2.transform.DOMove(midPosition, 1.0f).OnComplete(() =>
        {
            unitObj2.SetActive(false);
        });
   

        var unitListData = UserInfo.GetUnitListData();
        unitListData.Remove(unit1);
        unitListData.Remove(unit2);

    }
}
