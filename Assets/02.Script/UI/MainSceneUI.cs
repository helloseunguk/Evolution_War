using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public Button summonBtn;
    public Button specialSummonBtn;
    public Button mergeBtn;
    // Start is called before the first frame update
    void Start()
    {
        summonBtn.OnClickAsObservable().Subscribe(_ =>Managers.Spawn.SpawnUnit());

        specialSummonBtn.OnClickAsObservable().Subscribe(_ => Debug.Log("특별소환 버튼"));
        mergeBtn.OnClickAsObservable().Subscribe(_ => Debug.Log("머지버튼"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
