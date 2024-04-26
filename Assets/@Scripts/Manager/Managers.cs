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

    SpawnManager spawn = new SpawnManager();
    UnitManager unit = new UnitManager();
    StringManager stringManager = new StringManager();
    ResourceManager resourceManager = new ResourceManager();


    public static SpawnManager Spawn => Instance.spawn;
    public static UnitManager Unit => Instance.unit;
    public static StringManager String => Instance.stringManager; 
    public static ResourceManager Resource => Instance.resourceManager;
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

}
