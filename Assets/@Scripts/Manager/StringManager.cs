using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringManager 
{
    [Serializable]
    public class StringInfo 
    {
        public int id;
        public string str;
    }
    [Serializable]
    public class StringErrorInfo 
    {
        public int errorID;
        public int strID;
    }
    [Serializable]
    public class StringInfoTable 
    {
        public List<StringInfo> strList;
        public List<StringErrorInfo> strError;
    }

    const string strFontPath = "default/font";
    static Dictionary<int,string> strTableKor = new Dictionary<int,string>();
    static Dictionary<int, string> strTableEng = new Dictionary<int, string>();
    static Dictionary<int,string> strTableChn = new Dictionary<int, string>();
    static Dictionary<int, string> strTableJp = new Dictionary<int, string>();

    static Dictionary<Define.EFONT_TYPE, TMP_FontAsset> dicLoadedFonts = new Dictionary<Define.EFONT_TYPE, TMP_FontAsset>();
    static Dictionary<Define.EFONT_TYPE, Material> dicLoadedMaterials = new Dictionary<Define.EFONT_TYPE, Material>();

    public async UniTask LoadStringInfo() 
    {
        await LoadStringInfoScript();
    }
    public List<string> strException = new List<string>();

    public async UniTask LoadStringInfoScript() 
    {
        strTableKor.Clear();
        strTableEng.Clear();
        strTableChn.Clear();
        strTableJp.Clear();
        StringInfoTable stringInfo = null;

        var korean = await Managers.Resource.LoadAsync<TextAsset>("string", "stringKorean.json");
        {
            var resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + korean + "}");
            for (int i = 0; i < resultScript.result.Length; i++)
            {
                var stringScript = resultScript.result[i];
                strTableKor[stringScript.stringID] = stringScript.stringData;
            }
        }

        var english = await Managers.Resource.LoadAsync<TextAsset>("string", "stringEnglish.json");
        {
            var resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + english + "}");
            for (int i = 0; i < resultScript.result.Length; i++)
            {
                var stringScript = resultScript.result[i];
                strTableEng[stringScript.stringID] = stringScript.stringData;
            }
        }

        var japanese = await Managers.Resource.LoadAsync<TextAsset>("string", "stringJapanese.json");
        {
            var resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + japanese + "}");
            for (int i = 0; i < resultScript.result.Length; i++)
            {
                var stringScript = resultScript.result[i];
                strTableJp[stringScript.stringID] = stringScript.stringData;
            }
        }

        var chinese = await Managers.Resource.LoadAsync<TextAsset>("string", "stringChinese.json");
        {
            var resultScript = JsonUtility.FromJson<DataManager.StringScriptAll>("{ \"result\" : " + chinese + "}");
            for (int i = 0; i < resultScript.result.Length; i++)
            {
                var stringScript = resultScript.result[i];
                strTableChn[stringScript.stringID] = stringScript.stringData;
            }
        }

    }
    public string GetString(int strID)
    {
        string temp;
        if (strTableKor.TryGetValue(strID, out temp))
            return temp;
        else return null;
//        var systemLanguage = Managers.ServiceInfo.language;
//#if UNITY_EDITOR
//        systemLanguage = StringManagerEditor.GetLangaugeType();
//#endif
//        string temp;
//        switch (systemLanguage)
//        {
//            case SystemLanguage.Korean:
              
//                break;
//            case SystemLanguage.English:
//                if (strTableEng.TryGetValue(strID, out temp))
//                    return temp;
//                break;
//            case SystemLanguage.Chinese:
//            case SystemLanguage.ChineseSimplified:
//            case SystemLanguage.ChineseTraditional:
//                if (strTableChn.TryGetValue(strID, out temp))
//                    return temp;
//#if UNITY_EDITOR
//                else if (temp.IsNullOrEmpty() && strID > 0)
//                    return $"emptyStringID : {strID}";
//#endif
//                break;
//            case SystemLanguage.Japanese:
//                if (strTableJp.TryGetValue(strID, out temp))
//                    return temp;
//#if UNITY_EDITOR
//                else if (temp.IsNullOrEmpty() && strID > 0)
//                    return $"emptyStringID : {strID}";
//#endif
//                break;
//        }

//        return "";
    }
    public string GetBuildString(int _strID)
    {
        //   return Managers.ServiceInfo.GetServiceString(_strID);
        return null;
    }

    public string GetErrorString(int iErrorID)
    {
        //var id = Managers.Data.GetErrorDescript(iErrorID);
        //if (id == 0 || id == -1)
        //    return $"{GetString(25185)}({iErrorID})";

        //return $"{GetString(id)}({iErrorID})";
        return null;
    }
    public string GetString(int strID, Define.StringFileType _type)
    {
        switch (_type)
        {
            case Define.StringFileType.Normal:
                return GetString(strID);
            case Define.StringFileType.ErrorStr:
                return GetErrorString(strID);
            case Define.StringFileType.BuildStr:
                return GetBuildString(strID);
            default:
                break;
        }

        return GetString(strID);
    }
    public static TMP_FontAsset GetFont(Define.EFONT_TYPE _fontType)
    {
//        var systemLanguage = Managers.ServiceInfo.language;
//#if UNITY_EDITOR
//        systemLanguage = StringManagerEditor.GetLangaugeType();
//#endif
//        switch (systemLanguage)
//        {
//            case SystemLanguage.Korean:
//                return GetFontKorean(_fontType);
//            case SystemLanguage.English:
//             //   return GetFontEnglish(_fontType);
//            case SystemLanguage.Chinese:
//            case SystemLanguage.ChineseSimplified:
//            case SystemLanguage.ChineseTraditional:
//             //   return GetFontChinese(_fontType);
//            case SystemLanguage.Japanese:
//                // return GetFontJapanese(_fontType);
//                return null;
//            default:
//                return null;
//        }

        return GetFontKorean(_fontType);
    }
    public static TMP_FontAsset GetFontKorean(Define.EFONT_TYPE _fontType)
    {
        TMP_FontAsset font = null;
#if !UNITY_EDITOR
            if (dicLoadedFonts.TryGetValue(_fontType, out font))
            {
                if (font != null)
                {
                    return font;
                }
                else
                {
                    dicLoadedFonts.Remove(_fontType);
                }
            }
#endif
        switch (_fontType)
        {
            case Define.EFONT_TYPE.ENONE:
                break;
            case Define.EFONT_TYPE.Nanum_Line:
                break;
            case Define.EFONT_TYPE.Nanum_LineOpacity:
                break;

            case Define.EFONT_TYPE.Nanum_Default:
            case Define.EFONT_TYPE.Nanum_Line_White:
            case Define.EFONT_TYPE.Nanum_Line_Cream:
            case Define.EFONT_TYPE.Nanum_Line_Title:
            case Define.EFONT_TYPE.Nanum_Line_Opacity_Title:
            case Define.EFONT_TYPE.Nanum_Mesh_LineOpacity:
                break;
            case Define.EFONT_TYPE.En_Default:
            case Define.EFONT_TYPE.En_Line:
            case Define.EFONT_TYPE.En_LineOpacity:
            case Define.EFONT_TYPE.En_Shadow_Orange:
            case Define.EFONT_TYPE.En_Line_White:
            case Define.EFONT_TYPE.En_Line_Title:
            case Define.EFONT_TYPE.En_Line_Cream:
            case Define.EFONT_TYPE.COPRGTBSDF_Default:
            case Define.EFONT_TYPE.COPRGTBSDF_Line:
            case Define.EFONT_TYPE.COPRGTBSDF_LineOpacity:
            case Define.EFONT_TYPE.COPRGTBSDF_Shadow_Orange:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_White:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_Title:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_Cream:
                font = EAsset.LoadDefaultAsset<TMP_FontAsset>(strFontPath, "COPRGTBSDF.asset");
                break;
            case Define.EFONT_TYPE.Damage_LineOpacity:
                font = EAsset.LoadDefaultAsset<TMP_FontAsset>(strFontPath, "MADEDillanPERSONALUSESDF.asset");
                break;
            default:
                Debug.LogError(_fontType);
                throw new ArgumentOutOfRangeException(nameof(_fontType), _fontType, null);
        }

        if (font == null)
            font = EAsset.LoadDefaultAsset<TMP_FontAsset>(strFontPath, "NanumBarunGothicSDF.asset");
#if !UNITY_EDITOR
            dicLoadedFonts.Add(_fontType, font);
#endif
        return font;
    }
    public static Material GetMaterial(Define.EFONT_TYPE _fontType)
    {
//        var systemLanguage = Managers.ServiceInfo.language;
//#if UNITY_EDITOR
//        systemLanguage = StringManagerEditor.GetLangaugeType();
//#endif
//        switch (systemLanguage)
//        {
//            case SystemLanguage.Korean:
//                return GetMaterialKorean(_fontType);
//            case SystemLanguage.English:
//                return GetMaterialEnglish(_fontType);
//            case SystemLanguage.Chinese:
//            case SystemLanguage.ChineseSimplified:
//            case SystemLanguage.ChineseTraditional:
//                return GetMaterialChinese(_fontType);
//            case SystemLanguage.Japanese:
//                return GetMaterialJapanese(_fontType);
//        }

        return GetMaterialKorean(_fontType);
    }
    public static Material GetMaterialKorean(Define.EFONT_TYPE _fontType)
    {
        Material material = null;
#if !UNITY_EDITOR
            if (dicLoadedMaterials.TryGetValue(_fontType, out material))
            {
                if (material != null)
                {
                    return material;
                }

                dicLoadedMaterials.Remove(_fontType);
            }
#endif

        switch (_fontType)
        {
            case Define.EFONT_TYPE.Nanum_Default:
            case Define.EFONT_TYPE.ENONE:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_Default.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Line:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_Line.mat");
                break;
            case Define.EFONT_TYPE.Nanum_LineOpacity:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_LineOpacity.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Line_White:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_Line_White.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Line_Cream:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_Line_Cream.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Line_Title:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_LineTitle.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Line_Opacity_Title:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_LineOpacityTitle.mat");
                break;
            case Define.EFONT_TYPE.Nanum_Mesh_LineOpacity:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NanumBarunGothicSDF_Mesh_LineOpacity.mat");
                break;
            case Define.EFONT_TYPE.En_Default:
            case Define.EFONT_TYPE.COPRGTBSDF_Default:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_Default.mat");
                break;
            case Define.EFONT_TYPE.En_Line:
            case Define.EFONT_TYPE.COPRGTBSDF_Line:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_Line.mat");
                break;
            case Define.EFONT_TYPE.En_LineOpacity:
            case Define.EFONT_TYPE.COPRGTBSDF_LineOpacity:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_LineOpacity.mat");
                break;
            case Define.EFONT_TYPE.En_Shadow_Orange:
            case Define.EFONT_TYPE.COPRGTBSDF_Shadow_Orange:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_Shadow_Orange.mat");
                break;
            case Define.EFONT_TYPE.En_Line_White:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_White:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_Line_White.mat");
                break;
            case Define.EFONT_TYPE.En_Line_Title:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_Title:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_LineTitle.mat");
                break;
            case Define.EFONT_TYPE.En_Line_Cream:
            case Define.EFONT_TYPE.COPRGTBSDF_Line_Cream:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "COPRGTBSDF_Line_Cream.mat");
                break;

            case Define.EFONT_TYPE.JP_Default:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NotoSansJPTC-BoldSDF_Default.mat");
                break;

            case Define.EFONT_TYPE.TC_Default:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "NotoSansJPTC-BoldSDF_Default.mat");
                break;

            case Define.EFONT_TYPE.Damage_LineOpacity:
                material = EAsset.LoadDefaultAsset<Material>(strFontPath, "MADEDillanPERSONALUSESDF_LineOpacity.mat");
                break;
            default:
                return null;
        }

#if !UNITY_EDITOR
            dicLoadedMaterials.Add(_fontType, material);
#endif
        return material;
    }

}