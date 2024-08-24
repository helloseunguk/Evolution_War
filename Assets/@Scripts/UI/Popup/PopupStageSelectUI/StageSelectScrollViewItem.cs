using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSelectScrollViewItem : BaseScrollViewItem<StageInfoScript>
{

    public TMP_Text stageText;
    GameObject clearStar;
    public GameObject selectObj;
    public StageInfoScript info { get; private set; }

    private int index;

    public override void Init(StageInfoScript _info, int _index)
    {
        index = _index;
        info = _info;
        UpdateUI();
    }
    public void UpdateUI() 
    {
        stageText.text = string.Format(info.Stage +" - "+ info.Level);
    }
    public void OnSelect()
    {
        selectObj.gameObject.SetActive(true);
    }
    public void OnDeselect() 
    {
        selectObj.gameObject.SetActive(false);
    }
}
