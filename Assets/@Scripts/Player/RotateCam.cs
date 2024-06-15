using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCam : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public CinemachineVirtualCamera playerVirCam;
    private CinemachineOrbitalTransposer orbitalTransposer;

    public float rotationSpeed = 10f;

    Vector3 beginPos;
    Vector3 draggingPos;

    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    void Start()
    {
        orbitalTransposer = playerVirCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        if (orbitalTransposer == null)
        {
            Debug.LogError("CinemachineOrbitalTransposer 컴포넌트를 찾을 수 없습니다.");
        }
    }

    public void OnBeginDrag(PointerEventData beginPoint)
    {
        beginPos = beginPoint.position;

        xAngleTemp = xAngle;
        yAngleTemp = yAngle;
    }

    public void OnDrag(PointerEventData draggingPoint)
    {
        draggingPos = draggingPoint.position;

        // 슬라이드 방향에 따라 각도를 계산
        float deltaX = (draggingPos.x - beginPos.x) * rotationSpeed * Time.deltaTime;
        float deltaY = (draggingPos.y - beginPos.y) * rotationSpeed * Time.deltaTime;

        yAngle = yAngleTemp + deltaX;
        xAngle = xAngleTemp - deltaY;

        if (xAngle > 30) xAngle = 30;
        if (xAngle < -60) xAngle = -60;

        // orbitalTransposer의 m_XAxis 값을 직접 수정
        orbitalTransposer.m_XAxis.Value += deltaX;

        Debug.Log($"New XAxis Value: {orbitalTransposer.m_XAxis.Value}");
    }
}
