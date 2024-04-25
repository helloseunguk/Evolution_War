
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data", menuName ="Unit/Unit Data" , order = int.MaxValue)]
public class UnitData : ScriptableObject
{

    public int unitID { get; set; }

    public int unitLevel { get; set; }

    public string unitName { get; set; }

   public string unitDescription { get; set; }

    public int unitPrice { get; set; }

    public int unitType { get; set; }

    public GameObject prefab;

}
