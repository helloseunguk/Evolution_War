using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MainScene : BaseScene
{
    // Start is called before the first frame update
   public GameObject lobbyUI;
   public GameObject battleUI;

    public override void Start()
    {
        base.Start();
        Managers.Camera.RegistAllCamera();
        Managers.Camera.ActivateCamera("LobbyCamera");

        Managers.Battle.isStart.ObserveEveryValueChanged(_ =>_.Value).Subscribe(_ => 
        {
            if(_)
            {
                lobbyUI.SetActive(false);
            }
        });
        Managers.Battle.isArrived.ObserveEveryValueChanged(_ => _.Value).Subscribe(_ => 
        {
            if(_)
            {
                battleUI.SetActive(true);
            }
        });
    }

}
