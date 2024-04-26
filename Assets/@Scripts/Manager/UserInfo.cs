using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

static public class UserInfo 
{
    static int userCurUnitLevel;
    public static List<Unit> Units = new List<Unit>();
    static public int GetCurUnitLevel()
    {
        return userCurUnitLevel;    
    }
    //������ �����ϰ��ִ� ������ ������ �޾ƿ�
    static public List<Unit> GetUnitListData()
    {
        return Units;
    }
    static public void UpdateUnitListData(List<Unit> unitList) 
    {
        if(unitList != null)
        {
            Units = unitList;
        }
    }
    static public void RemoveUnitData(Unit unit) 
    {
         if(unit != null)
        {
            Units.Remove(unit);
        }
    }
    static public void SetUnitData(Unit unit) 
    {
        if(unit != null)
            Units.Add(unit);
    }
    public static void Clear() 
    {
        Units = new List<Unit>();
    }
}
