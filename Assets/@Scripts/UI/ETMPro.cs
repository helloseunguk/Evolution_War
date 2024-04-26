using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]
public class ETMPro : MonoBehaviour
{
    [OnValueChanged("SetEditString")]
    public int stringID = -1;

    public Define.EFONT_TYPE fontType = Define.EFONT_TYPE.None;
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
                //text.text = Managers.String
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
