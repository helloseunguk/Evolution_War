using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class PopupUserProfile : UI_Popup
{
    public ETMPro userName;
    public ETMPro userID;

    public override void Start()
    {
        base.Start();
        UpdateUI();
    }
    public void UpdateUI() 
    {
        userName.SetText("³ª´Â½Â¿í");
        userID.SetText("12321321321");
    }
}
