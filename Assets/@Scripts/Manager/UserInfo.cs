using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

static public class UserInfo 
{

    public static List<Unit> Units = new List<Unit>();

    public static UserData userData = new UserData();

    public static GameObject userHero;

    static public GameObject GetUserHero() 
    {
        return userHero;
    }
    //유저가 보유하고있는 유닛의 정보를 받아옴
    static public List<Unit> GetUnitListData()
    {
        return  Units;
        //return userData.unitList;
    }
    static public void RemoveUnitData(Unit unit) 
    {
         if(unit != null)
        {
            //userData.unitList.Remove(unit);
            Units.Remove(unit);
            userData.unitList.Remove(unit.Data);
        }
    }
    static public void AddUnitData(Unit unit) 
    {
        if (unit != null)
        {
            Units.Add(unit);
            userData.unitList.Add(unit.Data);
        }
    }
    public static void Clear() 
    {
       // userData.unitList = new List<Unit>();
        Units = new List<Unit>();
    }
    public static void RemoveDefaultUnits()
    {
        userData.unitList = userData.unitList.Where(unit => !unit.IsDefault()).ToList();
    }
}
