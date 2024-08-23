using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject claerRewardObj;
    public TMP_Text goldText;
    public TMP_Text gemText;

    public override void Start()
    {
        base.Start();
        claerRewardObj.SetActive(false);
        stageEnterBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Battle.OnStartBattle(stageSelectScrollView.selectedItem.info, arg.battlePosition);
            ClosePopupUI();
        });

        stageInfoScripts = Managers.Data.GetStageInfoScriptList;
        stageSelectScrollView.SetItemList(stageInfoScripts);
        stageSelectScrollView.OnItemClick.Subscribe(_ => 
        {
            claerRewardObj.SetActive(true);
  
            goldText.SetText(_.info.RewardGold.ToString());
            gemText.SetText(_.info.RewardGem.ToString());
        });
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
