using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum SceneType
    {
        None = 0,
        Title = 1,
        Loading = 2,
        MainScene = 3,

    }
    public enum AuthType 
    {
        None = 0,
        Authenticated = 1,  //로그인 상태
        UnAuthenticated = 2,//로그인 되지않은 상태


    }
    public enum SoundType
    {
        Master =0,
        Bgm = 1,
        AnotherBgm= 2,
        Effect = 3,
        Max = 4,
    }
    public enum MouseEvent
    {
        Press = 0,
        PointerDown = 1,
        PointerUp = 2,
        Click = 3,
    }
    //public enum AuthType
    //{
    //    None = 0,
    //    GuestAuth = 1, //게스트
    //    GoogleAuth = 2, //구글
    //    AppleAuth = 3, //애플
    //}
    public enum MarketType 
    {
        None = 0,
        AOS = 1,
        IOS = 2,
    }
    public enum EFONT_TYPE 
    {
        ENONE = 0,
        Nanum_Default = 1,
        Nanum_Line = 50,
        Nanum_LineOpacity = 51,
        Nanum_Line_White = 52,
        Nanum_Line_Cream = 53,
        Nanum_Line_Title = 54,
        Nanum_Line_Opacity_Title = 55,
        Nanum_Mesh_LineOpacity = 56, //인게임용 Material LineOpacity와 동일
        En_Default = 99,
        En_Line = 100,
        En_LineOpacity = 101,
        En_Shadow_Orange = 102,
        En_Line_White = 103,
        En_Line_Title = 104,
        En_Line_Cream = 105,
        JP_Default = 150,
        TC_Default = 176,
        Damage_LineOpacity = 200,
        COPRGTBSDF_Default = 201, // 한중일 공용 숫자,영문 폰트
        COPRGTBSDF_Line = 202,
        COPRGTBSDF_Line_Cream = 203,
        COPRGTBSDF_Line_White = 204,
        COPRGTBSDF_LineOpacity = 205,
        COPRGTBSDF_Line_Title = 206,
        COPRGTBSDF_Shadow_Orange = 207,

    }
    public enum StringFileType 
    {
        Normal = 0,
        ErrorStr = 1,
        BuildStr = 2,
    }
    public enum SystemLanguage
    {
        Afrikaans = 0,
        Arabic = 1,
        Basque = 2,
        Belarusian = 3,
        Bulgarian = 4,
        Catalan = 5,
        Chinese = 6,
        Czech = 7,
        Danish = 8,
        Dutch = 9,
        English = 10, //영어
        Estonian = 11,
        Faroese = 12,
        Finnish = 13,
        French = 14, //프랑스어
        German = 15,
        Greek = 16,
        Hebrew = 17,
        Hugarian = 18,
        Hungarian = 18,
        Icelandic = 19,
        Indonesian = 20,
        Italian = 21,
        Japanese = 22, //일본어
        Korean = 23, //한국어
        Latvian = 24,
        Lithuanian = 25,
        Norwegian = 26,
        Polish = 27,
        Portuguese = 28,
        Romanian = 29,
        Russian = 30,
        SerboCroatian = 31,
        Slovak = 32,
        Slovenian = 33,
        Spanish = 34,
        Swedish = 35,
        Thai = 36,
        Turkish = 37,
        Ukrainian = 38,
        Vietnamese = 39,
        ChineseSimplified = 40,
        ChineseTraditional = 41, //대만
        Unknown = 42,
    }
    public enum FadeDirection
    {
        None= 0,
        FadeIn = 1,
        FadeOut =2,
    }
    public enum FadeArrowType
    {
        None = 0,
        UpArrow = 1,
        DownArrow = 2,
        LeftArrow = 3,
        RightArrow = 4,
    }
    public enum SpawnRarity
    {
        None = 0,
        Normal = 1,
        Rare = 2,
        Hero = 3,
        Legendary = 4,
    }
    public enum AnimBoolType 
    {
        None = 0,
        isIdle = 1,
        isMove = 2,
        isDie = 3,
    }
    public enum AnimTriggerType
    {
        None = 0,
        onAttack = 1,
        
    }
}
