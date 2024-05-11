using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainSceneUI : MonoBehaviour
{
    public Button summonBtn;
    public Button specialSummonBtn;
    public Button mergeBtn;
    public UnitData unit;
    public Transform spawnPosition;

    public TMP_Text unitCountText;
    public TMP_Text totalPowerText;
    public TMP_Text ownGoldText;
    public TMP_Text ownGemText;
    public TMP_Text ownTicketText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(UserInfo.userData.gold + "cur Gold");
        UpdateUI();
        UserInfo.GetUnitListData().ObserveEveryValueChanged(units => units.Count)
                .Subscribe(_ => UpdateUI());
        summonBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Managers.Spawn.SpawnUnit(unit, spawnPosition.position, true);
            UpdateUI();
        });
        specialSummonBtn.OnClickAsObservable().Subscribe(_ => {
            Debug.Log("½ºÆä¼È ¼ÒÈ¯");
            UserInfo.userData.gold += 100;
            UserInfo.userData.gem += 50;

        });
        mergeBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Spawn.MergeUnit();
            UpdateUI();
        });
    }
    private void UpdateUI() 
    {
        unitCountText.SetText(UserInfo.GetUnitListData().Count.ToString());
        ownGoldText.SetText(UserInfo.userData.gold.ToString());
        ownGemText.SetText(UserInfo.userData.gem.ToString());
        ownTicketText.SetText(UserInfo.userData.ticket.ToString());
    }
}
