using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UIManager
{
    int order = 0;

    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    UI_Scene sceneUI = null;

    public void SetCanvas(GameObject go, bool sort = true) 
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        if(sort)
            canvas.sortingOrder = order++;
        else
            canvas.sortingOrder = 0;
    }
    public async UniTask<T> ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        if (UI_Canvas.SceneCanvas == null)
        {
            await Managers.Resource.Instantiate("SceneCanvas");
        }
        GameObject go = await Managers.Resource.Instantiate($"{name}", UI_Canvas.SceneCanvas.transform);

        T _sceneUI = Util.GetOrAddComponent<T>(go);
        sceneUI = _sceneUI;
        return _sceneUI;
    }
    public async UniTask<UI_Popup> ShowPopupUI(PopupType popupType)
    {
        string name = popupType.ToString();

        if (UI_Canvas.PopupCanvas == null)
        {
            await Managers.Resource.Instantiate("PopupCanvas");
        }
        GameObject go = await Managers.Resource.Instantiate($"{name}", UI_Canvas.PopupCanvas.transform);

        UI_Popup popup = Util.GetOrAddComponent<UI_Popup>(go);
        popupStack.Push(popup);
        return popup;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;
        if (popupStack.Peek() != popup)
        {
            Debug.LogError("Close Popup Failed");
            return;
        }
        ClosePopupUI();
    }
    public void ClosePopupUI() 
    {
        if (popupStack.Count == 0)
            return;
        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
    }
    public void CloseAllPopupUI() 
    {
        while(popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
}
