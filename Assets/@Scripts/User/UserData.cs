using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class UserData 
{
    public string name;
    public string id;
    public int gold;
    public int gem;
    public int ticket;
    public int level;
    public List<UnitData> unitList= new List<UnitData>();
}
