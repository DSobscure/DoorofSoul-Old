using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DoorofSoul.Library.General.ShadowComponents.UIComponents;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.IconScripts
{
    public class DraggableIcon : MonoBehaviour
    {
        private DraggableIconEventTrigger eventTrigger;
        public IUsableObject UsableObject { get; protected set; }
        private Text text;
        private DraggableIcon draggingIcon;
        private Vector2 pressPosition;

        void Awake()
        {
            eventTrigger = GetComponent<DraggableIconEventTrigger>();
            eventTrigger.OnDoubleClick += OnDoubleClick;
            eventTrigger.OnStartDrag += OnStartDrag;
            eventTrigger.OnStopDrag += OnStopDrag;
            eventTrigger.OnDragging += OnDragging;
            eventTrigger.OnDisplayUsableObject += OnDisplayDraggableIcon;

            text = GetComponentInChildren<Text>();
        }
        public void Initial(IUsableObject usableObject)
        {
            UsableObject = usableObject;
            text.text = UsableObject.Name;
        }

        private void OnDoubleClick(PointerEventData eventData)
        {

        }
        private void OnStartDrag(PointerEventData eventData)
        {
            pressPosition = eventData.pressPosition;

            draggingIcon = Instantiate(this);
            draggingIcon.transform.SetParent(GameObject.Find("Canvas").transform);
            draggingIcon.gameObject.layer = 0;
            RectTransform rectTransform = draggingIcon.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.position = transform.position;
            draggingIcon.GetComponent<Selectable>().interactable = false;
            draggingIcon.GetComponent<Image>().raycastTarget = false;
        }
        private void OnStopDrag(PointerEventData eventData)
        {
            Destroy(draggingIcon.gameObject);
        }
        private void OnDragging(PointerEventData eventData)
        {
            Vector3 displacement = eventData.position - pressPosition;
            draggingIcon.GetComponent<RectTransform>().position = transform.position + displacement;
        }
        protected virtual void OnDisplayDraggableIcon(IUsableObject usableObject)
        {
            if(usableObject != null)
            {
                Initial(usableObject);
            }
        }
    }
}
