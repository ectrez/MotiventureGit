using UnityEngine;
using UnityEngine.EventSystems;

public class DragHorizontalHall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform content;
    public RectTransform viewport;

    public float dragSpeed = 1f;
    public float smoothTime = 0.15f;

    private float minX;
    private float maxX;
    private float targetX;
    private float velocityX;

    void Start()
    {
        maxX = 0f;
        minX = -(content.rect.width - viewport.rect.width);
        targetX = content.anchoredPosition.x;
    }

    void Update()
    {
        Vector2 pos = content.anchoredPosition;

        pos.x = Mathf.SmoothDamp(pos.x, targetX, ref velocityX, smoothTime);
        content.anchoredPosition = pos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        velocityX = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetX += eventData.delta.x * dragSpeed;
        targetX = Mathf.Clamp(targetX, minX, maxX);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        targetX = Mathf.Clamp(targetX, minX, maxX);
    }
}