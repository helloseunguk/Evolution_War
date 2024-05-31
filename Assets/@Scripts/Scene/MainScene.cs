using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Managers.Camera.RegistAllCamera();
        Managers.Camera.ActivateCamera("LobbyCamera");
    }

}
