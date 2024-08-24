using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetTransZone : MonoBehaviour
{
    [Header("��ü�� ī�޶� �̸�")]
    public string transCameraName;
    [Header("���� üũ�� ��ũ")]
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
