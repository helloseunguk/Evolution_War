using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSpawnProbability", menuName = "Unit/UnitSpawnProbability")]
public class UnitSpawnProbability : ScriptableObject
{
    public float normalProbability = 0.6f;
    public float rareProbability = 0.25f;
    public float heroProbability = 0.1f;
    public float legendaryProbability = 0.05f;
}
