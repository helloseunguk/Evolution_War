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
    public List<UnitData> unitList;
    public Transform spawnPosition;

    public TMP_Text unitCountText;
    public TMP_Text totalPowerText;
    public TMP_Text ownGoldText;
    public TMP_Text ownGemText;
    public TMP_Text ownTicketText;
    public TMP_Text userLevelText;

    // Start is called before the first frame update
    void Start()
    {
        
        UpdateUI();
        UserInfo.SetUnitScriptData(unitList);
        UserInfo.GetUnitListData().ObserveEveryValueChanged(units => units.Count)
                .Subscribe(_ => UpdateUI());
        summonBtn.OnClickAsObservable().Subscribe(_ =>
        {
            int unitGrade = (UserInfo.userData.level - 1) / 5 + 1;  // 매 5 레벨마다 등급이 1씩 증가
            int unitLevel = (UserInfo.userData.level - 1) % 5 + 1;  // 레벨은 1에서 5까지 반복

  
            var unitData = unitList.Find(_ => _.level == unitLevel && _.grade == unitGrade);


            Managers.Spawn.SpawnUnit(unitData,spawnPosition.position,true);
            UpdateUI();
        });
        specialSummonBtn.OnClickAsObservable().Subscribe(_ => {
            Debug.Log("스페셜 소환");
            UserInfo.userData.gold += 100;
            UserInfo.userData.gem += 50;
            UserInfo.userData.level += 1;
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
        userLevelText.SetText(UserInfo.userData.level.ToString());
    }
}
