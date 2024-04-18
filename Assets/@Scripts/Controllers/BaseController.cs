using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected bool _init = false;
    protected Coroutine _coWait;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
