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
        UpdateUI();
        UserInfo.GetUnitListData().ObserveEveryValueChanged(units => units.Count)
                .Subscribe(_ => UpdateUI());
        summonBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Managers.Spawn.SpawnUnit(unit, spawnPosition.position, true);
            UpdateUI();
        });
        specialSummonBtn.OnClickAsObservable().Subscribe(_ => Debug.Log("½ºÆä¼È ¼ÒÈ¯") );
        mergeBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Spawn.MergeUnit();
            UpdateUI();
        });
    }
    private void UpdateUI() 
    {
        unitCountText.SetText(UserInfo.GetUnitListData().Count.ToString());
        // totalPowerText.SetText(ComputeTotalPower().ToString());
        // ownGoldText.SetText(UserInfo.Gold.ToString());
        // ownGemText.SetText(UserInfo.Gems.ToString());
        // ownTicketText.SetText(UserInfo.Tickets.ToString());
    }
}
