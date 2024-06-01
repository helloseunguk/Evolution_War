using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    //UnitData ����(��ũ���ͺ��� �ؽ��ڵ尡 ��� ���⶧��)
    public UnitData Data { get; set; }
    public GameObject GameObject { get; set; }
    public Unit(UnitData data, GameObject gameObject = null)
    {
        Data = data;
        if (gameObject != null)
            GameObject = gameObject;
    }

}
