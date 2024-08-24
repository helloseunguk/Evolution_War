using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetTransZone : MonoBehaviour
{
    [Header("교체될 카메라 이름")]
    public string transCameraName;
    [Header("엔터 체크할 태크")]
    public string compareTag;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(compareTag))
        {
            if(transCameraName != null) Managers.Camera.ActivateCamera(transCameraName);

            Managers.Camera.SetCameraTarget(EVUserInfo.GetUserHero().transform, EVUserInfo.GetUserHero().transform) ;

        }
    }
}
