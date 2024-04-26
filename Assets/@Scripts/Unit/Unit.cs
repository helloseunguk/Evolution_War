using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
  public UnitData Data { get; private set; }
    public GameObject GameObject { get; private set; }

    public int unitLevel { get; set; }
    public int unitGrade { get; set; }
    public Unit(UnitData data, GameObject gameObject)
    {
        Data = data;
        GameObject = gameObject;
    }

    public void MoveTo(Vector3 position)
    {
        GameObject.transform.position = position;
    }
}
