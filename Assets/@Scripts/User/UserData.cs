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
    public int level;
   
    public UserData() { }
    public UserData(string userName, string userId) 
    {
        this.name = userName;
        this.id = userId;
    }
}
