using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

#if UNITY_EDITOR
[InitializeOnLoad]
public class StringManagerEditor : MonoBehaviour
{
    public enum EEDITOR_LANGUAGE_TYPE
    {
        KOR,
        ENG,
        JP,
    }
    public enum EEDITOR_PACKAGE_TYPE
    {
        KOR,
        GLOBAL,
    }

    private const string MENU_LANGUAGE_NAME = "EV Tools/언어";
    private const string MENU_LANGUAGE_RESET = MENU_LANGUAGE_NAME + "/언어 리셋";
    private const string MENU_LANGUAGE_NAME_KOR_VIEW = MENU_LANGUAGE_NAME + "/한글";
    private const string MENU_LANGUAGE_NAME_ENG_VIEW = MENU_LANGUAGE_NAME + "/영어";

    private const string MENU_LANGUAGE_CHANGE = MENU_LANGUAGE_NAME + "/언어변경";
    private const string MENU_LANGUAGE_NAME_KOR = MENU_LANGUAGE_CHANGE + "/언어";
    private const string MENU_LANGUAGE_NAME_ENG = MENU_LANGUAGE_CHANGE + "/영어";

    private const string MENU_PACKAGE_NAME = "EV Tools/패키지 변경";
    private const string MENU_PACKAGE_NAME_KOR = MENU_PACKAGE_NAME + "/국내";
    private const string MENU_PACKAGE_NAME_ENG = MENU_PACKAGE_NAME + "/글로벌";

    public static EEDITOR_LANGUAGE_TYPE languageType;
    public static EEDITOR_PACKAGE_TYPE packageType;
    private static DataManager.StringScriptAll resultScript;

    static StringManagerEditor()
    {
        languageType = (EEDITOR_LANGUAGE_TYPE)EditorPrefs.GetInt(MENU_LANGUAGE_NAME, 0);

        packageType = (EEDITOR_PACKAGE_TYPE)EditorPrefs.GetInt(MENU_PACKAGE_NAME, 0);

        EditorApplication.delayCall += () =>
        {
            Menu.SetChecked(MENU_LANGUAGE_NAME_KOR, languageType == EEDITOR_LANGUAGE_TYPE.KOR);
            Menu.SetChecked(MENU_LANGUAGE_NAME_ENG, languageType == EEDITOR_LANGUAGE_TYPE.ENG);

            Menu.SetChecked(MENU_PACKAGE_NAME_KOR, packageType == EEDITOR_PACKAGE_TYPE.KOR);
            Menu.SetChecked(MENU_PACKAGE_NAME_ENG, packageType == EEDITOR_PACKAGE_TYPE.GLOBAL);

        };
    }
    #region 언어
    [MenuItem(MENU_LANGUAGE_NAME_KOR_VIEW)]
    private static void ViewLanguageKorean()
    {
        ViewLanguageType(EEDITOR_LANGUAGE_TYPE.KOR);
    }
    [MenuItem(MENU_LANGUAGE_NAME_ENG_VIEW)]
    private static void ViewLanguageEnglish()
    {
        ViewLanguageType(EEDITOR_LANGUAGE_TYPE.ENG);
    }

    #endregion
    public static EEDITOR_LANGUAGE_TYPE GetLanguageType(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.Korean:
                return EEDITOR_LANGUAGE_TYPE.KOR;
            case SystemLanguage.English:
                return EEDITOR_LANGUAGE_TYPE.ENG;  
            default:
                return EEDITOR_LANGUAGE_TYPE.KOR;
        }
    }
    public static string GetLanguageName(EEDITOR_LANGUAGE_TYPE languageType)
    {
        switch (languageType)
        {
            case EEDITOR_LANGUAGE_TYPE.KOR:
                return MENU_LANGUAGE_NAME_KOR;
            case EEDITOR_LANGUAGE_TYPE.ENG:
                return MENU_LANGUAGE_NAME_ENG;
            default:
                return MENU_LANGUAGE_NAME_KOR;
        }
    }
    public static SystemLanguage GetLangaugeType()
    {
        switch (languageType)
        {
            case EEDITOR_LANGUAGE_TYPE.KOR:
                return SystemLanguage.Korean;
            case EEDITOR_LANGUAGE_TYPE.ENG:
                return SystemLanguage.English;
        }

        return SystemLanguage.Korean;
    }
    public static string GetString(int stringID)
    {
            string path = "";
            if (GetLangaugeType() == SystemLanguage.English)
            {
                path = $"{EVAssetPath.assetPath}/scripts/string/stringEnglish.json";
            }
            else
            {
                path = $"{EVAssetPath.assetPath}/scripts/string/stringKorean.json";
            }


            TextAsset title = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            if (title != null)
            {
                resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + title.text + "}");

            }

        if (resultScript == null)
        {
            Debug.LogWarning("스트링파일을 찾지못했습니다");
            return "";
        }

        foreach (var mit in resultScript.result)
        {
            if (mit.stringID == stringID)
            {
                return mit.stringData;
                Debug.Log($"Find String {stringID} = {mit.stringData}");
                return mit.stringData;
            }
        }

        return "";
    }

    public static string GetString(int stringID, SystemLanguage systemLanguage)
    {
        DataManager.StringScriptAll _resultScript = null;
        string path = "";
        switch (systemLanguage)
        {
            case SystemLanguage.Korean:
                path = $"{EVAssetPath.assetPath}/scripts/string/stringKorean.json";
                break;
            case SystemLanguage.English:
                path = $"{EVAssetPath.assetPath}/scripts/string/stringEnglish.json";
                break;
            default:
                break;
        }

        TextAsset title = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        if (title != null)
        {
            _resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + title.text + "}");
        }

        if (_resultScript == null)
        {
            Debug.LogWarning("스트링파일을 찾지못했습니다");
            return "";
        }

        foreach (var mit in _resultScript.result)
        {
            if (mit.stringID == stringID)
            {
                return mit.stringData;

                Debug.Log($"Find String {stringID} = {mit.stringData}");
                return mit.stringData;
            }
        }

        return "";
    }

    private static void ViewLanguageType(EEDITOR_LANGUAGE_TYPE _eeditorLanguageType)
    {
        var lanType = languageType;
        languageType = _eeditorLanguageType;

        var currentStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (currentStage != null && currentStage.scene != null)
            currentStage.scene.GetRootGameObjects().DescendantsAndSelf().OfComponent<ETMPro>().ForEach(_1 =>
            {
                _1.SetEditString();
                EditorUtility.SetDirty(_1);
            });
        var scenePath = EditorSceneManager.GetActiveScene();
        if (scenePath != null)
            scenePath.GetRootGameObjects().DescendantsAndSelf().OfComponent<ETMPro>().ForEach(_1 => { _1.SetEditString(); });
        SceneView.RepaintAll();
        languageType = lanType;
    }
}

#endif