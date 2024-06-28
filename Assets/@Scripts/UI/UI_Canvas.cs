using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Canvas : MonoBehaviour
{
    public enum CanvasType
    {
        None,
        Master,
        Popup,
        Floating,
        Touch,
        Video,
        Tutorial,
    }
    private Canvas canvas;
    public CanvasType canvasType=  CanvasType.None;
    public static UI_Canvas SceneCanvas { get; private set; }
    public static UI_Canvas PopupCanvas { get; private set; }
    public bool setAspectRatio = false;

    public void Awake()
    {
        canvas = GetComponent<Canvas>();
        if (canvasType == CanvasType.Master)
        {
            SceneCanvas = this;
        }
        if (canvasType == CanvasType.Popup)
        {
            PopupCanvas = this;
        }

    }
}
