
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName ="Unit/Unit Data" , order = int.MaxValue)][System.Serializable]
public class UnitData : ScriptableObject
{
    public int grade;
    public int level;
    public int hp;
    public int damage;
    public int speed;
    public string color;
    public float attackSpeed;
    public float attackRange;

    public bool IsDefault()
    {
        return grade == 0 &&
               level == 0 &&
               hp == 0 &&
               damage == 0 &&
               speed == 0 &&
               string.IsNullOrEmpty(color) &&
               attackSpeed == 0.0f &&
               attackRange == 0.0f;
    }
}
