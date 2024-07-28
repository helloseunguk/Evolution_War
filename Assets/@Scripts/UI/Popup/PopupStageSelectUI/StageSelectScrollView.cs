using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScrollView : BaseScrollView<StageSelectScrollViewItem,StageInfoScript>
{
    public StageSelectScrollViewItem selectedItem;

    private void Start()
    {
        OnItemClick.Subscribe(_ => 
        {
            foreach(var mit in item)
            {
                if(mit.info.Stage == _.info.Stage && mit.info.Level == _.info.Level)
                {
                    mit.OnSelect();
                    selectedItem = mit;
                }
                else
                {
                    mit.OnDeselect();
                }
            }
        });
    }
    protected override void InitFirstItem(StageSelectScrollViewItem _obj)
    {
        var uiBtn = _obj.GetComponent<Button>();
        uiBtn.OnClickAsObservable().Subscribe(_ => { itemClickSubject.OnNext(_obj); });
    }
    protected override void InitItem(StageSelectScrollViewItem _obj, StageInfoScript _info, int _index)
    {
        base.InitItem(_obj, _info, _index);
    }
}
