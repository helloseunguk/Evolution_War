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

        // 랜덤 위치 생성: spawnPosition의 반경 10 이내
        if(isRandomPosition)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 10;  // 반경 10 이내의 랜덤 벡터 생성
            spawnPosition = new Vector3(spawnPosition.x + randomOffset.x,
                                                spawnPosition.y,
                                                spawnPosition.z + randomOffset.y);
        }

     //   GameObject unitObj = GameObject.Instantiate(_unit.prefab, spawnPosition, Quaternion.identity);

    //    Unit unit = new Unit(_unit, unitObj);

       // UserInfo.AddUnitData(unit);
      //  Managers.Unit.RegisterGameObject(unit, unitObj);
     


        //유닛 재활용
    }
    public void MergeUnit() 
    {
        //UserInfo.GetUnitList()로 현재 유저가 보유한 유닛 리스트를 받아온다
        var units = UserInfo.GetUnitListData();
        if (units.Count <2)
        {
            return;
        }
        //해당 리스트에서 가장 레벨 및 등급이 낮은 유닛 2개를 찾고 조건이 같다면 두 유닛 사이의 Position을 찾는다
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
        //Dotween으로 두 유닛위치를 보간처리하고 Position까지 1정도 남았을때 비활성화
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
        //비활성화와 동시에 한레벨 높은 유닛을 해당 Position에 소환
     
        //UserInfo에 유저가 보유하고있는 유닛 정보를 변경해줘야함
        UserInfo.UpdateUnitListData(unitListData);
    }
}
