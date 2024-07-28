using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("유닛 카운트")]
    public TMP_Text emenyCount;
    public TMP_Text teamCount;
    [Header("이미지 슬라이드")]
    public Image teamSlide;
    [Header("유닛 아이콘 이미지")]
    public Image enemyIcon;
    public Image teamIcon;
    [Header("유닛 아이콘 이미지 세팅값")]
    public float duration = 0.2f;
    public float scaleFactor = 1.2f;
    [Header("스테이지 정보")]
    public TMP_Text stageText;

    private Tween teamIconTwwen;
    private Tween enemyIconTwwen;   
    // Start is called before the first frame update
    void Start()
    {
        Managers.Battle.enemyUnitList.ObserveCountChanged().Subscribe(_ => 
        {
            Debug.Log("적 유닛의 카운트");
            emenyCount.text = _.ToString();
            OnEnemyCountChange();
            CheckBattleState(_);

        }).AddTo(this);
        Managers.Battle.teamUnitList.ObserveCountChanged().Subscribe(_ =>
        {
            Debug.Log("아군 유닛의 카운트");
            teamCount.text = _.ToString();
            OnTeamCountChange();
            CheckBattleState(_);

        }).AddTo(this);
    }
    private void OnEnable()
    {
        teamCount.text = Managers.Battle.teamUnitList.Count.ToString();
        emenyCount.text = Managers.Battle.enemyUnitList.Count.ToString();
        stageText.text = string.Format(Managers.Battle.GetCurStageInfo().Stage + " - " +Managers.Battle.GetCurStageInfo().Level);
    }
    private void CheckBattleState(int unitCount) 
    {
        if(unitCount <=0)
        {
            Managers.Battle.isDone.Value = true;
        }
    }
    private void OnTeamCountChange() 
    {
        if (teamIconTwwen != null && teamIconTwwen.IsPlaying())
        {
            teamIconTwwen.Kill();
        }

        // 새로운 애니메이션 시작
        teamIconTwwen = teamIcon.rectTransform.DOScale(Vector3.one * scaleFactor, duration)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // 애니메이션 완료 후 원래 크기로 돌아가도록 설정
                teamIcon.rectTransform.localScale = Vector3.one;
            });
    }
    private void OnEnemyCountChange()
    {
        if(enemyIconTwwen != null && enemyIconTwwen.IsPlaying())
        {
            enemyIconTwwen.Kill();
        }

        enemyIconTwwen = enemyIcon.rectTransform.DOScale(Vector3.one * scaleFactor, duration)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                enemyIcon.rectTransform.localScale = Vector3.one;
            });
   
    }
}
