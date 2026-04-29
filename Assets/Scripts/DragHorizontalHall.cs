using UnityEngine;
using UnityEngine.EventSystems;

public class DragHorizontalHall : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [Header("Hall Object")]
    public RectTransform hallContent;

    [Header("Movement Limits")]
    public float maxRightX = 965f;
    public float maxLeftX = -923f;
    public float fixedY = -20f;

    private float pointerStartX;
    private float hallStartX;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartX = eventData.position.x;
        hallStartX = hallContent.localPosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float pointerDeltaX = eventData.position.x - pointerStartX;

        float newX = hallStartX + pointerDeltaX;

        newX = Mathf.Clamp(newX, maxLeftX, maxRightX);

        hallContent.localPosition = new Vector3(newX, fixedY, 0f);
    }
}