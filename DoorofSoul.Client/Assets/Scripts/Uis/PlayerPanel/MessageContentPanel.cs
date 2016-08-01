using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageContentPanel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform self;
    private bool canDrag;
    private float originMousePositionX;
    private float originPositionX;

    void Start ()
    {
        self = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            float newPositionX = Mathf.Min(Mathf.Max(originPositionX + eventData.position.x - originMousePositionX, -Screen.width / 2 + self.sizeDelta.x / 2), Screen.width / 2 - self.sizeDelta.x / 2);
            self.localPosition = new Vector2(newPositionX, self.localPosition.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
        originMousePositionX = eventData.position.x;
        originPositionX = self.localPosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canDrag = false;
    }
}
