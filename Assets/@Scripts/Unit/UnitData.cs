
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName ="Unit/Unit Data" , order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public int grade;
    public int level;
    public int hp;
    public int damage;
    public int speed;
    public string color;
}
