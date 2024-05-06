using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UserData 
{
    public string name;
    public string id;

    public UserData() { }
    public UserData(string userName, string userId) 
    {
        this.name = userName;
        this.id = userId;
    }
}
