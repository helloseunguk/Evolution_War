using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager 
{
    public void SpawnUnit(UnitData _unit) 
    {
        Debug.Log("���� ��ȯ");
        if (_unit == null)
            return;

        GameObject unit = GameObject.Instantiate(_unit.prefab);
        unit.name = _unit.name;

       
        //���� ��Ȱ��
    }
    public void MergeUnit() 
    {
        //���� ��Ȱ��
    }
}
