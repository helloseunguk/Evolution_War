using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    //UnitData ����(��ũ���ͺ��� �ؽ��ڵ尡 ��� ���⶧��)
  public UnitData Data { get; set; }
    public int unitLevel { get; set; }
    public int unitGrade { get; set; }
    public Unit(UnitData data)
    {
        Data = data;
    }

}
