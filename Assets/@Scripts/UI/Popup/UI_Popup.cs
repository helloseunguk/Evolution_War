using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{
    public Button backBtn;

    private void Start()
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
    public virtual void ClosePopupUI() 
    {
        Managers.UI.ClosePopupUI(this);
    }
}
