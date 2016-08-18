using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.UiScripts.PlayerPanelScripts
{
    public class MessageContentPanel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private RectTransform self;
        private bool canDrag;
        private float originMousePositionX;
        private float originPositionX;
        private ScrollTextArea messageContent;
        private Text heightHelperText;

        void Awake()
        {
            self = GetComponent<RectTransform>();
            messageContent = transform.FindChild("ScrollTextArea").GetComponent<ScrollTextArea>();
            heightHelperText = transform.FindChild("HeightHelperText").GetComponent<Text>();
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
        public void ShowMessage(string text)
        {
            heightHelperText.text = text;
            messageContent.textAreaSize.y = heightHelperText.preferredHeight;
            messageContent.textAreaPosition.y = messageContent.size.y - heightHelperText.preferredHeight + heightHelperText.fontSize + heightHelperText.lineSpacing;
            messageContent.ShowText(text);
        }
    }
}
