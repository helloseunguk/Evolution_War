using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private SpawnManager spawnManager;

    public SpawnManager Spawn { get { return spawnManager; } }
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = new SpawnManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
