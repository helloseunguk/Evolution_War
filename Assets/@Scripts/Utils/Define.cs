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
    public enum AuthType
    {
        None = 0,
        GuestAuth = 1, //게스트
        GoogleAuth = 2, //구글
        AppleAuth = 3, //애플
    }
    public enum MarketType 
    {
        None = 0,
        AOS = 1,
        IOS = 2,
    }
    public enum EFONT_TYPE 
    {
        None = 0,
        Nanum_Default = 1,
        Nanum_Line = 2,

    }
    public enum StringFileType 
    {
        Normal = 0,
        ErrorStr = 1,
        BuildStr = 2,
    }
}
