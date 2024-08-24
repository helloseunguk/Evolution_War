using Cysharp.Text;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class StringManager
{
    static Dictionary<int, string> strTableKor = new Dictionary<int, string>();
    static Dictionary<int, string> strTableEng = new Dictionary<int, string>();

    static Dictionary<Define.EFONT_TYPE, TMP_FontAsset> dicLoadedFonts = new Dictionary<Define.EFONT_TYPE, TMP_FontAsset>();
    static Dictionary<Define.EFONT_TYPE, Material> dicLoadedMaterial = new Dictionary<Define.EFONT_TYPE, Material>();

    public async UniTask LoadStringInfo()
    {
        await LoadStringInfoScript();
    }

    public List<string> strException = new List<string>();

    static async UniTask LoadStringInfoScript()
    {
        await LoadStringInfoScriptToLanguage();
    }
    public static async UniTask LoadStringInfoScriptToLanguage()
    {
        strTableEng.Clear();
        strTableKor.Clear();

        var systemLanguage = Managers.ServiceInfo.language;
        var stringInfo = await Managers.Resource.LoadAssetAsync<TextAsset>(ZString.Format("string{0}.json", systemLanguage));
        var resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + stringInfo + "}");

        for (int i = 0; i < resultScript.result.Length; i++)
        {
            var stringScript = resultScript.result[i];
            int id = stringScript.stringID;
            string data = stringScript.stringData;

            switch (systemLanguage)
            {
                case SystemLanguage.Korean:
                    strTableKor[id] = data;
                    break;
                case SystemLanguage.English:
                    strTableEng[id] = data;
                    break;
            }
        }
    }
    public string GetString(int strID)
    {
        var systemLanguage = Managers.ServiceInfo.language;
        string temp = string.Empty;
        switch (systemLanguage)
        {
            case SystemLanguage.Korean:
                strTableKor.TryGetValue(strID, out temp);
                break;
            case SystemLanguage.English:
                strTableEng.TryGetValue(strID, out temp);
                break;
        }
        return temp;
    }
    public string GetString(int strID, Define.StringFileType _type)
    {
        switch (_type)
        {
            case Define.StringFileType.Normal:
                return GetString(strID);
            default:
                break;
        }

        return GetString(strID);
    }
    public int GetStringLength(int strID)
    {
        var systemLanguage = Managers.ServiceInfo.language;
#if UNITY_EDITOR
        systemLanguage = StringManagerEditor.GetLangaugeType();
#endif
        string temp;
        int size = 0;
        switch (systemLanguage)
        {
            case SystemLanguage.Korean:
                if (strTableKor.TryGetValue(strID, out temp))
                    size = temp.Length;
                break;
            case SystemLanguage.English:
                if (strTableEng.TryGetValue(strID, out temp))
                    size = temp.Length;
                break;
        }

        return size;
    }
  
}