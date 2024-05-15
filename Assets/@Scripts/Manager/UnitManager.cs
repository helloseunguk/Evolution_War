using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager 
{
    private static Dictionary<Unit, GameObject> unitGameObjectDict = new Dictionary<Unit, GameObject>();

    public void RegisterGameObject(Unit unitData, GameObject unitObj)
    {
        if(unitData != null & unitObj != null)
        {
            unitGameObjectDict[unitData] = unitObj;
        }
    }
    public GameObject GetUnitObject(Unit unitData)
    {
        unitGameObjectDict.TryGetValue(unitData, out GameObject unitObj);
        return unitObj;
    }
}
