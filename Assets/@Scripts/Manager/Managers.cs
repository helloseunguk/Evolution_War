using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_Instance;

    public static Managers Instance 
    {
        get 
        {
            Init();
            return s_Instance;
        }
    }
    private SpawnManager spawnManager;
    private UnitManager unitManager;

    SpawnManager spawn = new SpawnManager();
    UnitManager unit = new UnitManager();
    public static SpawnManager Spawn => Instance.spawnManager;
    public static UnitManager Unit => Instance.unitManager;
    static void Init() 
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
                go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = new SpawnManager();
        unitManager = new UnitManager();
    }
}
