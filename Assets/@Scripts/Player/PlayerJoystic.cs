using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystic : MonoBehaviour
{
    public Transform playerTransform;
    public Transform playerCameraTransform;

    public float speed = 10f;

    public Joystick controller;

    public PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        // 카메라의 forward와 right 벡터를 기준으로 이동 방향을 계산
        Vector3 forward = playerCameraTransform.forward;
        Vector3 right = playerCameraTransform.right;

        // 카메라의 y 축 회전만 사용하고, 다른 축은 무시
        forward.y = 0;
        right.y = 0;

        // 정규화하여 벡터의 길이를 1로 만듦
        forward.Normalize();
        right.Normalize();

        // 조이스틱 입력에 따라 이동 방향을 계산
        Vector3 moveDir = forward * controller.Vertical + right * controller.Horizontal;

        // 입력이 없는 경우, 움직이지 않음
        if (moveDir == Vector3.zero)
        {
            playerMovement.isMove.Value = false;
            return;
        }
        else
        {
            playerMovement.isMove.Value = true;
        }

        // 캐릭터의 회전과 이동을 설정
        playerTransform.rotation = Quaternion.LookRotation(moveDir);
        playerTransform.Translate(moveDir * Time.fixedDeltaTime * speed, Space.World);
    }
}
