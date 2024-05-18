using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit 
{
    //UnitData 랩핑(스크립터블은 해쉬코드가 모두 같기때문)
  public UnitData Data { get; set; }
    public Unit(UnitData data)
    {
        Data = data;
    }

}
