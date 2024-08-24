using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class PopupArg
{
    public static readonly PopupArg empty = new PopupArg();
}
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
    public async UniTask<UI_Popup> ShowPopupUI(Define.PopupType popupType, PopupArg arg = null)
    {
        string name = popupType.ToString();

        if (UI_Canvas.PopupCanvas == null)
        {
            await Managers.Resource.Instantiate("PopupCanvas");
        }
        GameObject go = await Managers.Resource.Instantiate($"{name}", UI_Canvas.PopupCanvas.transform);

        UI_Popup popup = Util.GetOrAddComponent<UI_Popup>(go);
        if(arg != null)
        {
            popup.SetPopupArg(arg);
        }
        popupStack.Push(popup);
        return popup;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
        {
            Debug.LogWarning("No popups to close.");
            return;
        }

        if (popupStack.Peek() != popup)
        {
            Debug.LogError("Close Popup Failed: The popup to close is not the top popup.");
            return;
        }

        ClosePopupUI();
    }
    public void ClosePopupUI() 
    {
        if (popupStack.Count == 0)
        {
            Debug.LogWarning("No popups to close.");
            return;
        }

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
