using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour
{
    private GameObject currentDraggingObject;
    private Vector3 offset;
    private float initialYPosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // 카메라는 시작할 때 한 번만 참조
        HandleDragEvents();
    }

    private void HandleDragEvents()
    {
        // 마우스 버튼을 눌렀을 때의 이벤트 처리
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Subscribe(_ => HandleMouseDown());

        // 마우스 버튼을 누르고 있는 동안의 이벤트 처리
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0) && currentDraggingObject != null)
            .Subscribe(_ => HandleMouseDrag());

        // 마우스 버튼을 놓았을 때의 이벤트 처리
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonUp(0) && currentDraggingObject != null)
            .Subscribe(_ => HandleMouseUp());
    }

    private void HandleMouseDown()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.CompareTag("Unit"))
        {
            StartDragging(hit);
        }
    }

    private void HandleMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(mainCamera.transform.position, currentDraggingObject.transform.position);
        Vector3 objectPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        objectPosition.y = initialYPosition + 1f; // Y 위치를 초기 Y 위치보다 1 높게 고정합니다.
        currentDraggingObject.transform.position = objectPosition;
    }

    private void HandleMouseUp()
    {
        currentDraggingObject = null;
    }

    private void StartDragging(RaycastHit hit)
    {
        currentDraggingObject = hit.transform.gameObject;
        initialYPosition = currentDraggingObject.transform.position.y; // 초기 Y 위치를 저장합니다.
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance));
        offset = currentDraggingObject.transform.position - mouseWorldPosition;
        offset.y = 0; // Y 오프셋은 사용하지 않습니다.
    }
}