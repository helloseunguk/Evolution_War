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
        Init();
        UpdateUI();
        var unitList = Managers.Data.GetUnitInfoScript();

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
    private void Init()
    {
        List<UnitData> unitsToRemove = new List<UnitData>();

        foreach (var unit in UserInfo.userData.unitList)
        {
            // 유닛을 스폰
            Managers.Spawn.SpawnUnit(unit, spawnPosition.position, true);
            // 제거할 유닛을 임시 리스트에 추가
            unitsToRemove.Add(unit);
        }

        // 모든 유닛을 스폰한 후, 임시 리스트에서 unitList에서 제거
        foreach (var unit in unitsToRemove)
        {
            UserInfo.userData.unitList.Remove(unit);
        }
    }
}
