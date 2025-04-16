using UnityEngine;
using System.Collections.Generic;

public class UnitSelection : MonoBehaviour
{
    public LayerMask unitMask;
    public LayerMask groundMask;
    public RectTransform selectionBox;
    public Canvas canvas;  // อย่าลืมลาก Canvas มาใส่ใน Inspector

    private Vector2 startPos;
    private bool isDragging = false;

    private List<Unit> selectedUnits = new List<Unit>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out startPos
            );

            selectionBox.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out currentMousePos
            );

            Vector2 size = currentMousePos - startPos;

            selectionBox.anchoredPosition = startPos;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            selectionBox.pivot = new Vector2(size.x >= 0 ? 0 : 1, size.y >= 0 ? 0 : 1);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
            SelectUnitsInBox();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
            {
                foreach (Unit unit in selectedUnits)
                {
                    unit.MoveTo(hit.point);
                }
            }
        }
    }

    void ClearSelection()
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.Deselect();
        }

        selectedUnits.Clear();
    }

    void SelectUnitsInBox()
    {
        ClearSelection();

        Vector2 min = selectionBox.anchoredPosition;
        Vector2 max = min + selectionBox.sizeDelta;

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            if (localPoint.x >= min.x && localPoint.x <= max.x &&
                localPoint.y >= min.y && localPoint.y <= max.y)
            {
                selectedUnits.Add(unit);
                unit.Select();
            }
        }
    }
}
