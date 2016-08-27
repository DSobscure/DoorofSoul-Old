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
        protected DraggableIconEventTrigger eventTrigger;
        public IUsableObject UsableObject { get; protected set; }
        protected Text iconText;
        private DraggableIcon draggingIcon;
        private Vector2 pressPosition;
        void Awake()
        {
            Instantiate();
        }
        public virtual void Instantiate()
        {
            eventTrigger = GetComponent<DraggableIconEventTrigger>();
            eventTrigger.OnDoubleClick += OnDoubleClick;
            eventTrigger.OnStartDrag += OnStartDrag;
            eventTrigger.OnStopDrag += OnStopDrag;
            eventTrigger.OnDragging += OnDragging;
            eventTrigger.OnDisplayUsableObject += OnDisplayDraggableIcon;

            iconText = transform.FindChild("IconText").GetComponent<Text>();
        }

        public virtual void Initial(IUsableObject usableObject)
        {
            UsableObject = usableObject;
            if (usableObject != null)
            {
                iconText.text = UsableObject.Name;
            }
            else
            {
                iconText.text = "";
            }
        }

        protected virtual void OnDoubleClick(PointerEventData eventData)
        {

        }
        protected virtual void OnStartDrag(PointerEventData eventData)
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
        protected virtual void OnStopDrag(PointerEventData eventData)
        {
            if(draggingIcon != null)
            {
                Destroy(draggingIcon.gameObject);
                draggingIcon = null;
            }
        }
        protected virtual void OnDragging(PointerEventData eventData)
        {
            if (draggingIcon != null)
            {
                Vector3 displacement = eventData.position - pressPosition;
                draggingIcon.GetComponent<RectTransform>().position = transform.position + displacement;
            }
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
