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
    AuthManager authManager = new AuthManager();
    DataManager data = new DataManager();
    BattleManager battle = new BattleManager();
    CameraManager camera = new CameraManager();

    public static SpawnManager Spawn => Instance.spawn;
    public static UnitManager Unit => Instance.unit;
    public static StringManager String => Instance.stringManager; 
    public static ResourceManager Resource => Instance.resourceManager;
    public static AuthManager AuthManager => Instance.authManager;
    public static DataManager Data => Instance.data;
    public static BattleManager Battle => Instance.battle;
    public static CameraManager Camera => Instance.camera;

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

            Data.Init();
        }
    }
    private void OnApplicationQuit()
    {
        Data.OnApplicationQuit();
    }
}
