using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager 
{
    private Dictionary<string, CinemachineVirtualCamera> cameras;
    private CinemachineVirtualCameraBase activeCamera;
    public void RegistAllCamera() 
    {
        cameras = new Dictionary<string, CinemachineVirtualCamera>();

        // 씬에 있는 모든 CinemachineVirtualCamera를 찾아 등록
        CinemachineVirtualCamera[] foundCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var cam in foundCameras)
        {
            cameras.Add(cam.name, cam);
        }
    }
    public void ActivateCamera(string cameraName)
    {
        if (cameras.ContainsKey(cameraName))
        {
            foreach (var cam in cameras.Values)
            {
                cam.gameObject.SetActive(false);
            }
            activeCamera = cameras[cameraName];
            activeCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Camera with name {cameraName} not found.");
        }
    }
    public void RefreshCameras()
    {
        cameras.Clear();
        RegistAllCamera();
    }
    // 특정 카메라의 파라미터 변경 (예: 줌 레벨)
    public void SetCameraFieldOfView(string cameraName, float fov)
    {
        if (cameras.ContainsKey(cameraName))
        {
            cameras[cameraName].m_Lens.FieldOfView = fov;
        }
        else
        {
            Debug.LogWarning($"Camera with name {cameraName} not found.");
        }
    }
    // 현재 활성화된 카메라의 타겟 설정
    public void SetCameraTarget(Transform followTarget, Transform lookAtTarget)
    {
        if (activeCamera != null)
        {
            if (activeCamera is CinemachineVirtualCamera virtualCamera)
            {
                virtualCamera.Follow = followTarget;
                virtualCamera.LookAt = lookAtTarget;
            }
            else if (activeCamera is CinemachineFreeLook freeLookCamera)
            {
                freeLookCamera.Follow = followTarget;
                freeLookCamera.LookAt = lookAtTarget;
            }
        }
        else
        {
            Debug.LogWarning("No active camera found.");
        }
    }
}
