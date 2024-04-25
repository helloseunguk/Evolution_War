using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager 
{
    public void SpawnUnit(UnitData _unit) 
    {
        Debug.Log("¿Ø¥÷ º“»Ø");
        if (_unit == null)
            return;

        GameObject unit = GameObject.Instantiate(_unit.prefab);
        unit.name = _unit.name;

       
        //¿Ø¥÷ ¿Á»∞øÎ
    }
    public void MergeUnit() 
    {
        //¿Ø¥÷ ¿Á»∞øÎ
    }
}
