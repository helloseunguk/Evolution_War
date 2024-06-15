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
        Init();
        UpdateUI();

        UserInfo.GetUnitListData().ObserveEveryValueChanged(units => units.Count)
                .Subscribe(_ => UpdateUI());

        UserInfo.userHero = hero;

        summonBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Managers.Spawn.SpawnUnit(spawnTransform.position, true, spawnTransform);
            UpdateUI();
        }).AddTo(this);
        specialSummonBtn.OnClickAsObservable().Subscribe(_ => {
            Debug.Log("스페셜 소환");
            UserInfo.userData.gold += 100;
            UserInfo.userData.gem += 50;
            UserInfo.userData.level += 1;
        }).AddTo(this);
        mergeBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Managers.Spawn.MergeUnit(spawnTransform);
            UpdateUI();
        }).AddTo(this);
        battleBtn.OnClickAsObservable().Subscribe(_ => 
        {
            Debug.Log("배틀 시작");
            Managers.Battle.isStart.Value = true;
            Managers.Battle.InitBattleHero(heroBattlePosition.transform.position);
            Managers.Battle.InitBattleUnit(battlePosition.transform.position);
            Managers.Battle.InitBattleEnemy(battlePosition.transform.position);
        }).AddTo(this);
    }
    private void UpdateUI() 
    {
        unitCountText.SetText(UserInfo.GetUnitListData().Count.ToString());
        ownGoldText.SetText(UserInfo.userData.gold.ToString());
        ownGemText.SetText(UserInfo.userData.gem.ToString());
        ownTicketText.SetText(UserInfo.userData.ticket.ToString());
    //    userLevelText.SetText(UserInfo.userData.level.ToString());
    }
    private void Init()
    {
        List<UnitData> unitsToRemove = new List<UnitData>();

        foreach (var unit in UserInfo.userData.unitList)
        {
            // 유닛을 스폰
            Managers.Spawn.SpawnUnit(spawnTransform.position, true, spawnTransform);
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
