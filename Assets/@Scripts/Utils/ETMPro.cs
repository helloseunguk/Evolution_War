using Cysharp.Text;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]
public class ETMPro : MonoBehaviour
{
    [OnValueChanged("SetEditString")]
    public int stringID = -1;

    public Define.EFONT_TYPE fontType = Define.EFONT_TYPE.ENONE;

    public TMP_Text text;
    public Define.StringFileType strFileType = Define.StringFileType.Normal;

    [NonSerialized]
    public bool isSetText = false;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        if(text)
        {
            if(stringID != -1 && isSetText == false)
            {
                text.text = Managers.String.GetString(stringID, strFileType);
            }
        }
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;
        SetEditString();
    }

    [Button("스트링적용", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
    public void SetEditString()
    {
        text = GetComponent<TMP_Text>();

        if (text)
        {
            if (stringID != -1)
            {
                //    NMAuth.LoadData();
                text.text = StringManagerEditor.GetString(stringID);
                Debug.Log(StringManagerEditor.GetString(stringID));
            }
        }
    }
#endif
    public void SetRefresh()
    {
        if (text)
        {
            var lan = Managers.ServiceInfo.language;

            text.text = Managers.String.GetString(stringID, strFileType);
        }
    }
    public void SetStringID(int _id)
    {
        stringID = _id;
        if (text)
        {
            text.text = Managers.String.GetString(stringID, strFileType);
        }
    }
    public void SetStringID(int _id, Define.EFONT_TYPE _font)
    {
        fontType = _font;
        //text.font = StringManager.GetFont(fontType);
        //text.fontMaterial = StringManager.GetMaterial(fontType);
        SetStringID(_id);
    }
    public void SetStringID(int _id, Color _color)
    {
        SetColor(_color);
        SetStringID(_id);
    }

    public void SetFontType(Define.EFONT_TYPE _font)
    {
        fontType = _font;
        //text.font = StringManager.GetFont(fontType);
        //text.fontMaterial = StringManager.GetMaterial(fontType);
    }

    public void SetColor(Color _color)
    {
        isSetText = true;
        if (text)
        {
            text.color = _color;
        }
    }

    public void SetText<T1>(T1 arg1)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1);
        }
    }

    public void SetText<T1, T2>(T1 arg1, T2 arg2)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(format, arg1, arg2);
        }
    }

    public void SetText<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3);
        }
    }

    public void SetText<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4);
        }
    }

    public void SetText<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        isSetText = true;
        if (text)
        {
            string format = Managers.String.GetString(stringID, strFileType);
            if (string.IsNullOrEmpty(format) == false)
                text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
        T10 arg10)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
        T10 arg10, T11 arg11)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
        T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11, arg12);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
        T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
        T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15);
        }
    }

    public void SetText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
    {
        isSetText = true;
        if (text)
        {
            text.text = ZString.Format(Managers.String.GetString(stringID, strFileType), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10,
                arg11, arg12, arg13, arg14, arg15, arg16);
        }
    }
}
