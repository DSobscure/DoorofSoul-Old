using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DoorofSoul.Library.General.ShadowComponents.UIComponents;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.IconScripts
{
    public class DraggableIconEventTrigger : EventTrigger
    {
        private float doubleClickTimeSpan = 0.2f;
        private float lastClickTime;
        private int consecutiveClickCount = 1;

        private event Action<PointerEventData> onDoubleClick;
        public event Action<PointerEventData> OnDoubleClick { add { onDoubleClick += value; } remove { onDoubleClick -= value; } }

        private event Action<PointerEventData> onStartDrag;
        public event Action<PointerEventData> OnStartDrag { add { onStartDrag += value; } remove { onStartDrag -= value; } }

        private event Action<PointerEventData> onStopDrag;
        public event Action<PointerEventData> OnStopDrag { add { onStopDrag += value; } remove { onStopDrag -= value; } }

        private event Action<PointerEventData> onDragging;
        public event Action<PointerEventData> OnDragging { add { onDragging += value; } remove { onDragging -= value; } }

        private event Action<IUsableObject> onDisplayUsableObject;
        public event Action<IUsableObject> OnDisplayUsableObject { add { onDisplayUsableObject += value; } remove { onDisplayUsableObject -= value; } }

        protected DraggableIcon draggableIconForDisplay;

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (eventData.clickTime - lastClickTime < doubleClickTimeSpan)
            {
                consecutiveClickCount++;
                if(consecutiveClickCount == 2)
                {
                    if(onDoubleClick != null)
                        onDoubleClick.Invoke(eventData);
                }
            }
            else
            {
                consecutiveClickCount = 1;
            }
            lastClickTime = eventData.clickTime;
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (onStartDrag != null)
                onStartDrag.Invoke(eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (onStopDrag != null)
                onStopDrag.Invoke(eventData);
            EventSystem.current.SetSelectedGameObject(null);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (onDragging != null)
                onDragging.Invoke(eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if(eventData.selectedObject != null && eventData.selectedObject.GetComponent<DraggableIcon>() != null)
            {
                draggableIconForDisplay = eventData.selectedObject.GetComponent<DraggableIcon>();
            }
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            draggableIconForDisplay = null;
        }
        public override void OnDrop(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (draggableIconForDisplay != null)
            {
                if (onDisplayUsableObject != null)
                {
                    onDisplayUsableObject.Invoke(draggableIconForDisplay.UsableObject);
                }
            }
        }
    }
}
