using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{
    public Button backBtn;
    public PopupArg popupArg { get; private set; }
    public virtual void Start()
    {
        backBtn.OnClickAsObservable().Subscribe(_ =>
        {
            ClosePopupUI();
        });
    }
    public virtual void Init() 
    {
        Managers.UI.SetCanvas(gameObject, true);
    }
    public virtual void SetPopupArg(PopupArg arg)
    {
        popupArg = arg;
    }
    public virtual void ClosePopupUI() 
    {
        Managers.UI.ClosePopupUI(this);
    }
}
