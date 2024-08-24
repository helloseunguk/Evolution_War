using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AGameService
{
    public string clientIP;

}
public class ClientLogData
{
    public Int64 AccountID { get; set; }
    public string StrLog { get; set; }
}
public class RGameService
{
    public string serviceVersion;
    public Define.RuntimePlatfirm platformType;
    public Define.AreaGroupType areaGroup;
    public Define.SystemLanguage systemLanguage;

}

public class ServiceInfoManager 
{
    public Define.AreaGroupType area = Define.AreaGroupType.Asia;

    public bool LanguagelsRange(List<SystemLanguage> languages)
    {
        if (languages.IsNullOrEmpty())
            return false;

        return languages.Contains(Application.systemLanguage);
    }

    public SystemLanguage language
    {
        get
        {
            var lan = SystemLanguage.Unknown;
#if UNITY_EDITOR
            lan = StringManagerEditor.GetLangaugeType();
            return lan;
#endif
            var lanLong = PlayerPrefs.GetInt(EVOption.LocalLanguageOptionString, 0);
            lan = (SystemLanguage)lanLong;
            return lan;
        }
    }
}
