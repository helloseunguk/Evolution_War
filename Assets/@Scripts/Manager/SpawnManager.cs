using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class SpawnManager 
{
    UnitData unitData;
    public void SpawnUnit(UnitData _unit,Vector3 spawnPosition,bool isRandomPosition) 
    {
        if (_unit == null)
            return;
        unitData = _unit;

        // ���� ��ġ ����: spawnPosition�� �ݰ� 10 �̳�
        if(isRandomPosition)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 10;  // �ݰ� 10 �̳��� ���� ���� ����
            spawnPosition = new Vector3(spawnPosition.x + randomOffset.x,
                                                spawnPosition.y,
                                                spawnPosition.z + randomOffset.y);
        }

     //   GameObject unitObj = GameObject.Instantiate(_unit.prefab, spawnPosition, Quaternion.identity);

    //    Unit unit = new Unit(_unit, unitObj);

       // UserInfo.AddUnitData(unit);
      //  Managers.Unit.RegisterGameObject(unit, unitObj);
     


        //���� ��Ȱ��
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
        var sortUnits = units.OrderBy(_ => _.unitLevel).ThenBy(_ => _.unitGrade).ToList();

        var unit1 = sortUnits[0];
        var unit2 = sortUnits[1];

        if (unit1.unitLevel != unit2.unitLevel)
            return;
        if (unit1.unitGrade != unit2.unitGrade)
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
        unitObj2.transform.DOMove(midPosition, 1.0f).OnComplete(() => unitObj2.SetActive(false));
   

        var unitListData = UserInfo.GetUnitListData();
        unitListData.Remove(unit1);
        unitListData.Remove(unit2);
        //��Ȱ��ȭ�� ���ÿ� �ѷ��� ���� ������ �ش� Position�� ��ȯ
     
        //UserInfo�� ������ �����ϰ��ִ� ���� ������ �����������
        UserInfo.UpdateUnitListData(unitListData);
    }
}
