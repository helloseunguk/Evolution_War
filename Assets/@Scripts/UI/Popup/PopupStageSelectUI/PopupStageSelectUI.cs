using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class PBStageSelectUI : PopupArg
{
    public Vector3 battlePosition;

}
public class PopupStageSelectUI : UI_Popup
{
    public StageSelectScrollView stageSelectScrollView;
    List<StageInfoScript> stageInfoScripts = new List<StageInfoScript>();
    public Button stageEnterBtn;
    private PBStageSelectUI arg;
    public override void Start()
    {
        base.Start();
        stageEnterBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Battle.OnStartBattle(stageSelectScrollView.selectedItem.info, arg.battlePosition);
            ClosePopupUI();
        });

        stageInfoScripts = Managers.Data.GetStageInfoScriptList;
        stageSelectScrollView.SetItemList(stageInfoScripts);

        arg = popupArg as PBStageSelectUI;
        if (arg == null)
        {
            Debug.Log("PBStage Arg is Null");
        }
    }
    public override void SetPopupArg(PopupArg arg)
    {
        base.SetPopupArg(arg);
        this.arg = arg as PBStageSelectUI;

        if (this.arg == null)
        {
            Debug.LogError("Invalid argument type passed to PopupStageSelectUI.");
        }
    }
}
