using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts;

namespace DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.ExtraPanelScripts
{
    public class InventoryPanel : MonoBehaviour, IEventProvider
    {
        [SerializeField]
        private InventoryItemInfoIcon inventoryItemInfoIconPrefab;
        private Inventory inventory;
        private ScrollRect inventoryItemsScrollView;
        private RectTransform inventoryItemsContent;
        private Button closeButton;
        public int columnCount = 4;

        private InventoryItemInfoIcon[] inventoryItemInfoIcons;

        void OnDestroy()
        {
            EraseEvents();
        }

        public void BindInventory(Inventory inventory)
        {
            this.inventory = inventory;
            inventoryItemInfoIcons = new InventoryItemInfoIcon[inventory.Capacity];
            inventoryItemsScrollView = transform.FindChild("InventoryItemsScrollView").GetComponent<ScrollRect>();
            inventoryItemsContent = inventoryItemsScrollView.transform.FindChild("Viewport").FindChild("InventoryItemsContent").GetComponent<RectTransform>();
            closeButton = transform.FindChild("TitleBar").FindChild("CloseButton").GetComponent<Button>();
            closeButton.onClick.AddListener(() => Destroy(gameObject));
            RegisterEvents();
            ShowInventory();
        }

        private void ShowInventory()
        {
            inventoryItemsContent.transform.ClearChildren();
            Vector2 blockSize = inventoryItemInfoIconPrefab.GetComponent<RectTransform>().sizeDelta;
            inventoryItemsContent.sizeDelta = new Vector2((blockSize.x + 2) * columnCount + 2, (blockSize.y + 2) * inventory.Capacity / columnCount + 2);
            inventoryItemsContent.position = new Vector2(-inventoryItemsContent.sizeDelta.x / 2, 0);
            for (int i = 0; i < inventory.Capacity; i++)
            {
                inventoryItemInfoIcons[i] = Instantiate(inventoryItemInfoIconPrefab);
                RectTransform blockRectTransform = inventoryItemInfoIcons[i].GetComponent<RectTransform>();
                blockRectTransform.transform.SetParent(inventoryItemsContent);
                blockRectTransform.localScale = Vector3.one;
                blockRectTransform.anchorMin = new Vector2(0, 1);
                blockRectTransform.anchorMax = new Vector2(0, 1);
                blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                float x = 1 + (blockRectTransform.sizeDelta.x + 2) * ((i % columnCount) + 1) - blockRectTransform.sizeDelta.x / 2;
                float y = 1 + (blockRectTransform.sizeDelta.y + 2) * ((i / columnCount) + 1) - blockRectTransform.sizeDelta.y / 2;
                blockRectTransform.localPosition = new Vector2(x, -y);
                int index = i;
                inventoryItemInfoIcons[i].Initial(new InventoryItemInfo { positionIndex = i });
            }
            foreach (var info in inventory.ItemInfos)
            {
                inventoryItemInfoIcons[info.positionIndex].Initial(info);
            }
        }
        private void OnItemChange(InventoryItemInfo info)
        {
            if (info.positionIndex >= 0 && info.positionIndex < inventory.Capacity)
            {
                inventoryItemInfoIcons[info.positionIndex].Initial(info);
            }
        }

        public void RegisterEvents()
        {
            inventory.OnItemChange += OnItemChange;
        }

        public void EraseEvents()
        {
            inventory.OnItemChange -= OnItemChange;
        }

        public void Close()
        {
            closeButton.onClick.Invoke();
        }
    }
}
