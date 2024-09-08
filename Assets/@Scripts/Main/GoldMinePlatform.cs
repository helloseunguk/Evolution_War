using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GoldMinePlatform : MonoBehaviour
{
    public Button platformBtn;
    public ReactiveProperty<bool> isMining = new ReactiveProperty<bool>(false);
    public Vector3 basePosition;
    UnitAgent miningAgent = null;
    FloatingBase currentFloatingText;

    void Start()
    {
        platformBtn = GetComponent<Button>();
        platformBtn.OnClickAsObservable().Subscribe(_ =>
        {
            if (isMining.Value == false)
            {
                UnitAgent[] allUnitAgents = FindObjectsOfType<UnitAgent>();

                if (allUnitAgents.Length == 0)
                {
                    return;
                }

                foreach (var agent in allUnitAgents)
                {
                    if (agent.unitData == null) continue;
                    if (agent.isGoldMining == true) continue; 

                    if (miningAgent == null)
                    {
                        miningAgent = agent;
                    }
                    else if (agent.unitData.grade > miningAgent.unitData.grade)
                    {
                        miningAgent = agent;
                    }
                    else if (agent.unitData.grade == miningAgent.unitData.grade && agent.unitData.level > miningAgent.unitData.level)
                    {
                        miningAgent = agent;
                    }
                }


                if (miningAgent != null)
                {
                    basePosition = miningAgent.transform.position;
                    miningAgent.transform.position = this.transform.position;
                    miningAgent.isGoldMining = true;
                    isMining.Value = true;
                }
            }
            else
            {
                isMining.Value = false;
                miningAgent.isGoldMining = false;
                miningAgent.transform.position = basePosition;
                miningAgent = null;
            }
        })
        .AddTo(this);

        isMining.Subscribe(value =>
        {
            if (value == true)
            {
                currentFloatingText = Managers.Floating.OnFloatingText(this.transform, miningAgent.unitData.goldIncome, true);
            }
            else
            {
                if (currentFloatingText != null)
                {
                    Managers.Floating.ReturnFloatingText(currentFloatingText);
                    currentFloatingText = null;
                }
            }
        }).AddTo(this);
    }
}
