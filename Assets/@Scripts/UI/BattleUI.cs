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
    [Header("���� ī��Ʈ")]
    public TMP_Text emenyCount;
    public TMP_Text teamCount;
    [Header("�̹��� �����̵�")]
    public Image teamSlide;
    [Header("���� ������ �̹���")]
    public Image enemyIcon;
    public Image teamIcon;
    [Header("���� ������ �̹��� ���ð�")]
    public float duration = 0.2f;
    public float scaleFactor = 1.2f;
    [Header("�������� ����")]
    public TMP_Text stageText;

    private Tween teamIconTwwen;
    private Tween enemyIconTwwen;   
    // Start is called before the first frame update
    void Start()
    {
        Managers.Battle.enemyUnitList.ObserveCountChanged().Subscribe(_ => 
        {
            Debug.Log("�� ������ ī��Ʈ");
            emenyCount.text = _.ToString();
            OnEnemyCountChange();
            CheckBattleState(_);

        }).AddTo(this);
        Managers.Battle.teamUnitList.ObserveCountChanged().Subscribe(_ =>
        {
            Debug.Log("�Ʊ� ������ ī��Ʈ");
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

        // ���ο� �ִϸ��̼� ����
        teamIconTwwen = teamIcon.rectTransform.DOScale(Vector3.one * scaleFactor, duration)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // �ִϸ��̼� �Ϸ� �� ���� ũ��� ���ư����� ����
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
