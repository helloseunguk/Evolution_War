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
        mainCamera = Camera.main; // ī�޶�� ������ �� �� ���� ����
        HandleDragEvents();
    }

    private void HandleDragEvents()
    {
        // ���콺 ��ư�� ������ ���� �̺�Ʈ ó��
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Subscribe(_ => HandleMouseDown());

        // ���콺 ��ư�� ������ �ִ� ������ �̺�Ʈ ó��
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0) && currentDraggingObject != null)
            .Subscribe(_ => HandleMouseDrag());

        // ���콺 ��ư�� ������ ���� �̺�Ʈ ó��
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
        objectPosition.y = initialYPosition + 1f; // Y ��ġ�� �ʱ� Y ��ġ���� 1 ���� �����մϴ�.
        currentDraggingObject.transform.position = objectPosition;
    }

    private void HandleMouseUp()
    {
        currentDraggingObject = null;
    }

    private void StartDragging(RaycastHit hit)
    {
        currentDraggingObject = hit.transform.gameObject;
        initialYPosition = currentDraggingObject.transform.position.y; // �ʱ� Y ��ġ�� �����մϴ�.
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance));
        offset = currentDraggingObject.transform.position - mouseWorldPosition;
        offset.y = 0; // Y �������� ������� �ʽ��ϴ�.
    }
}