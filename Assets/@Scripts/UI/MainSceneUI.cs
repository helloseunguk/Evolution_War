using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
public class MainSceneUI : MonoBehaviour
{
    public Button summonBtn;
    public Button specialSummonBtn;
    public Button mergeBtn;
    public Button battleBtn;
    public Button battleStageBtn;
    public Button userProfileBtn;

    public Transform spawnTransform;

    public TMP_Text unitCountText;
    public TMP_Text totalPowerText;
    public TMP_Text ownGoldText;
    public TMP_Text ownGemText;
    public TMP_Text ownTicketText;
    public TMP_Text userLevelText;

    public GameObject lobbyCamera;
    public GameObject battleCamera;
    public GameObject battlePosition;
    public GameObject heroBattlePosition;

    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();

        EWUserInfo.GetUnitListData().ObserveEveryValueChanged(units => units.Count)
                .Subscribe(_ => UpdateUI());

        EWUserInfo.userHero = hero;

        summonBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Managers.Spawn.SpawnUnit(spawnTransform.position, true,true, spawnTransform);
            UpdateUI();
        }).AddTo(this);
        specialSummonBtn.OnClickAsObservable().Subscribe(_ => {
            Debug.Log("½ºÆä¼È ¼ÒÈ¯");
            EWUserInfo.userData.gold += 100;
            EWUserInfo.userData.gem += 50;
            EWUserInfo.userData.level += 1;
        }).AddTo(this);
        mergeBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Spawn.MergeUnit(spawnTransform);
            UpdateUI();
        }).AddTo(this);
        battleStageBtn.OnClickAsObservable().Subscribe(async _ => 
        {
            //await Managers.UI.ShowPopupUI(Define.PopupType.PopupStageSelectUI, new PBStageSelectUI
            //{
            //    battlePosition = battlePosition.transform.position
            //});
            await Managers.UI.ShowPopupUI(Define.PopupType.PopupStageSelectUI, new PBStageSelectUI 
            {
                battlePosition = battlePosition.transform.position
            });
        });
        userProfileBtn.OnClickAsObservable().Subscribe(async _ => 
        {
            await Managers.UI.ShowPopupUI(Define.PopupType.PopupUserProfile, PopupArg.empty);
        });
    }
    private void UpdateUI() 
    {
        unitCountText.SetText(EWUserInfo.GetUnitListData().Count.ToString());
        ownGoldText.SetText(EWUserInfo.userData.gold.ToString());
        ownGemText.SetText(EWUserInfo.userData.gem.ToString());
        ownTicketText.SetText(EWUserInfo.userData.ticket.ToString());
    //    userLevelText.SetText(UserInfo.userData.level.ToString());
    }
   
}
