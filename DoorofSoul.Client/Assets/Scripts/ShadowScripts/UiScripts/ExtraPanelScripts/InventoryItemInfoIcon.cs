using System;
using System.Collections.Generic;
using UnityEngine;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using UnityEngine.UI;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.IconScripts;
using DoorofSoul.Library.General.ShadowComponents.UIComponents;
using UnityEngine.EventSystems;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts
{
    public class InventoryItemInfoIcon : DraggableIcon
    {
        private Text itemCountText;

        void Awake()
        {
            Instantiate();
            itemCountText = transform.FindChild("ItemCountText").GetComponent<Text>();
        }
        public override void Instantiate()
        {
            eventTrigger = GetComponent<InventoryItemInfoIconEventTrigger>();
            eventTrigger.OnDoubleClick += OnDoubleClick;
            eventTrigger.OnStartDrag += OnStartDrag;
            eventTrigger.OnStopDrag += OnStopDrag;
            eventTrigger.OnDragging += OnDragging;
            eventTrigger.OnDisplayUsableObject += OnDisplayDraggableIcon;
            eventTrigger.OnDoubleClick += OnUseItem;

            iconText = transform.FindChild("IconText").GetComponent<Text>();
        }
        public override void Initial(IUsableObject usableObject)
        {
            UsableObject = usableObject;
            InventoryItemInfo info = usableObject as InventoryItemInfo;
            
            if(info != null && info.item != null)
            {
                iconText.text = info.Name;
                itemCountText.text = info.count.ToString();
            }
            else
            {
                iconText.text = "";
                itemCountText.text = "";
            }
        }
        protected override void OnStartDrag(PointerEventData eventData)
        {
            if((UsableObject as InventoryItemInfo).item != null)
            {
                base.OnStartDrag(eventData);
            }
        }
        protected override void OnStopDrag(PointerEventData eventData)
        {
            base.OnStopDrag(eventData);
            if(eventData.pointerCurrentRaycast.gameObject == null)
            {
                InventoryItemInfo selfInfo = UsableObject as InventoryItemInfo;
                Global.Global.Seat.MainContainer.ContainerOperationManager.DiscardItem(selfInfo.positionIndex);
            }
        }
        protected override void OnDisplayDraggableIcon(IUsableObject usableObject)
        {
            InventoryItemInfo enterInfo = usableObject as InventoryItemInfo;
            InventoryItemInfo selfInfo = UsableObject as InventoryItemInfo;
            Global.Global.Seat.MainContainer.ContainerOperationManager.MoveInventoryItemInfo(enterInfo.positionIndex, selfInfo.positionIndex);
        }
        private void OnUseItem(PointerEventData eventData)
        {
            InventoryItemInfo selfInfo = UsableObject as InventoryItemInfo;
            if (selfInfo.item != null)
            {
                Global.Global.Seat.MainContainer.ContainerOperationManager.UseItem(selfInfo.positionIndex);
            }
        }
    }
}
