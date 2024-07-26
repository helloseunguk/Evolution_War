using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EWSettingScriptableObject : ScriptableObject
{

    private const string settingFolderPath = "Assets/Resources";
    private const string settingFilePath = "Assets/Resources/EWSetting.asset";

    public int editorResourceLoadDelayFrame = 1;
    private static EWSettingScriptableObject instance;
    public static EWSettingScriptableObject Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance = Resources.Load<EWSettingScriptableObject>("EWSetting");
#if UNITY_EDITOR
            if(instance ==null)
            {
                if(!AssetDatabase.IsValidFolder(settingFolderPath))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }
                instance = AssetDatabase.LoadAssetAtPath<EWSettingScriptableObject>(settingFilePath);
                if (instance == null)
                {
                    instance = CreateInstance<EWSettingScriptableObject>();
                    AssetDatabase.CreateAsset(instance, settingFilePath);
                }
            }
#endif
            return instance;
        }
    }
}
