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

    UnitAgent targetUnit;
    private void Start()
    {
        mainCamera = Camera.main; // 카메라는 시작할 때 한 번만 참조
        HandleDragEvents();
    }

    private void HandleDragEvents()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            .Subscribe(_ => HandleMouseDown());

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0) && currentDraggingObject != null)
            .Subscribe(_ => HandleMouseDrag());

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
            targetUnit = hit.transform.GetComponent<UnitAgent>();
            targetUnit.OnOutline(Outline.Mode.OutlineAll, Color.white, 1.5f);

            var allUnit = GameObject.FindObjectsOfType<UnitAgent>();
            foreach(var unit in allUnit)
            {
                if (unit == targetUnit) continue;
                if(unit.isPlayable) continue;

                if (unit.unitData.grade ==targetUnit.unitData.grade && unit.unitData.level == targetUnit.unitData.level)
                {
                    unit.OnOutline(Outline.Mode.OutlineAll, Color.white, 1.5f);
                }
            }
        }
    }

    private void HandleMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(mainCamera.transform.position, currentDraggingObject.transform.position);
        Vector3 objectPosition = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        objectPosition.y = initialYPosition + 1f;
        currentDraggingObject.transform.position = objectPosition;
    }

    private void HandleMouseUp()
    {
        currentDraggingObject = null;
        var allUnit = GameObject.FindObjectsOfType<UnitAgent>();
        foreach (var unit in allUnit)
        {
            if (unit.isPlayable) continue;
            unit.OffOutline();
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Draw the ray in red for visualization (it will only be visible in the Scene view)
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 2f); // Draw for 2 seconds

        // Raycast to get all objects hit
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        // Loop through all raycast hits
        foreach (var hit in hits)
        {
            // If the hit object is a unit
            if (hit.transform.CompareTag("Unit"))
            {
                UnitAgent hitUnit = hit.transform.GetComponent<UnitAgent>();
                if (hitUnit == null) break;
                if (targetUnit == null) break;
                if (hitUnit.isPlayable) continue;

                // If the selected unit and hit unit have the same grade and level
                if (hitUnit.unitData.grade == targetUnit.unitData.grade && hitUnit.unitData.level == targetUnit.unitData.level)
                {
                    Managers.Spawn.MergeUnit(targetUnit, hitUnit);
                }
            }
        }

        targetUnit = null;
    }


    private void StartDragging(RaycastHit hit)
    {
        currentDraggingObject = hit.transform.gameObject;
        initialYPosition = currentDraggingObject.transform.position.y;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, hit.distance));
        offset = currentDraggingObject.transform.position - mouseWorldPosition;
        offset.y = 0;
    }
}