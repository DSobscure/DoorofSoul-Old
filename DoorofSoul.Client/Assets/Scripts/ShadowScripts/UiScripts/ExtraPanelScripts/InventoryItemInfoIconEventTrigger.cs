using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.IconScripts;
using UnityEngine.EventSystems;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts
{
    public class InventoryItemInfoIconEventTrigger : DraggableIconEventTrigger
    {
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (eventData.selectedObject != null && eventData.selectedObject.GetComponent<InventoryItemInfoIcon>() != null)
            {
                draggableIconForDisplay = eventData.selectedObject.GetComponent<InventoryItemInfoIcon>();
            }
        }
    }
}
