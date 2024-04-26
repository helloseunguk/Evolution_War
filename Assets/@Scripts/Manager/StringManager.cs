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

        //var korean = await Managers.Resour
    }
}