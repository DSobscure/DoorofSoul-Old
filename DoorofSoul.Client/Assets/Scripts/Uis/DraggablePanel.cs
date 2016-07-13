using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePanel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool canDrag;
    private Vector3 originMousePosition;
    private Vector3 originPosition;
    private RectTransform self;

    void Start()
    {
        self = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            Vector3 newPosition = originPosition + Input.mousePosition - originMousePosition;
            if (newPosition.x < -Screen.width / 2 + self.rect.width / 2)
                newPosition.x = -Screen.width / 2 + self.rect.width / 2;
            else if (newPosition.x > Screen.width / 2 - self.rect.width / 2)
                newPosition.x = Screen.width / 2 - self.rect.width / 2;
            if (newPosition.y < -Screen.height / 2 + self.rect.height / 2)
                newPosition.y = -Screen.height / 2 + self.rect.height / 2;
            else if (newPosition.y > Screen.height / 2 - self.rect.height / 2)
                newPosition.y = Screen.height / 2 - self.rect.height / 2;
            self.localPosition = newPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canDrag = true;
        originMousePosition = Input.mousePosition;
        originPosition = self.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canDrag = false;
    }
}
