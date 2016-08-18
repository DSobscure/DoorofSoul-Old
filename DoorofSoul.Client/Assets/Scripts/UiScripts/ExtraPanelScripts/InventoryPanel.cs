using UnityEngine;
using UnityEngine.UI;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Client.HelpFunctions;
using DoorofSoul.Client.Interfaces;
using System;

namespace DoorofSoul.Client.Scripts.UiScripts.ExtraPanelScripts
{
    public class InventoryPanel : MonoBehaviour, IEventProvider
    {
        [SerializeField]
        private Button inventoryItemBlockButtonPrefab;
        private Inventory inventory;
        private ScrollRect inventoryItemsScrollView;
        private RectTransform inventoryItemsContent;
        private Button closeButton;
        public int columnCount = 4;

        private Button[] inventoryItemBlockButtons;
        private int selectedIndex = -1;

        void OnDestroy()
        {
            EraseEvents();
        }

        public void BindInventory(Inventory inventory)
        {
            this.inventory = inventory;
            inventoryItemBlockButtons = new Button[inventory.Capacity];
            inventoryItemsScrollView = transform.FindChild("InventoryItemsScrollView").GetComponent<ScrollRect>();
            inventoryItemsContent = inventoryItemsScrollView.transform.FindChild("Viewport").FindChild("InventoryItemsContent").GetComponent<RectTransform>();
            closeButton = transform.FindChild("TitleBar").FindChild("CloseButton").GetComponent<Button>();
            closeButton.onClick.AddListener(() => Destroy(gameObject));
            RegisterEvents();
            ShowInventory();
        }

        private void ShowInventory()
        {
            inventoryItemsContent.transform.ClearChild();
            Vector2 blockSize = inventoryItemBlockButtonPrefab.GetComponent<RectTransform>().sizeDelta;
            inventoryItemsContent.sizeDelta = new Vector2((blockSize.x + 2) * columnCount + 2, (blockSize.y + 2) * inventory.Capacity / columnCount + 2);
            inventoryItemsContent.position = new Vector2(-inventoryItemsContent.sizeDelta.x / 2, 0);
            for (int i = 0; i < inventory.Capacity; i++)
            {
                inventoryItemBlockButtons[i] = Instantiate(inventoryItemBlockButtonPrefab);
                RectTransform blockRectTransform = inventoryItemBlockButtons[i].GetComponent<RectTransform>();
                blockRectTransform.transform.SetParent(inventoryItemsContent);
                blockRectTransform.localScale = Vector3.one;
                blockRectTransform.anchorMin = new Vector2(0, 1);
                blockRectTransform.anchorMax = new Vector2(0, 1);
                blockRectTransform.pivot = new Vector2(0.5f, 0.5f);
                float x = 1 + (blockRectTransform.sizeDelta.x + 2) * ((i % columnCount) + 1) - blockRectTransform.sizeDelta.x / 2;
                float y = 1 + (blockRectTransform.sizeDelta.y + 2) * ((i / columnCount) + 1) - blockRectTransform.sizeDelta.y / 2;
                blockRectTransform.localPosition = new Vector2(x, -y);
                int index = i;
                inventoryItemBlockButtons[i].onClick.AddListener(() => SelectBlock(index));
            }
            foreach (var info in inventory.ItemInfos)
            {
                inventoryItemBlockButtons[info.positionIndex].transform.FindChild("ItemNameText").GetComponent<Text>().text = info.item.ItemName;
                inventoryItemBlockButtons[info.positionIndex].transform.FindChild("ItemCountText").GetComponent<Text>().text = info.count.ToString();
            }
        }
        private void SelectBlock(int index)
        {
            if (selectedIndex >= 0)
            {
                inventoryItemBlockButtons[selectedIndex].GetComponent<Image>().color = inventoryItemBlockButtonPrefab.GetComponent<Image>().color;
            }
            selectedIndex = index;
            inventoryItemBlockButtons[selectedIndex].GetComponent<Image>().color = new Color(50, 50, 50, 50);
        }
        private void OnItemChange(ItemInfo info)
        {
            if (info.positionIndex >= 0 && info.positionIndex < inventory.Capacity)
            {
                inventoryItemBlockButtons[info.positionIndex].transform.FindChild("ItemNameText").GetComponent<Text>().text = info.item.ItemName;
                inventoryItemBlockButtons[info.positionIndex].transform.FindChild("ItemCountText").GetComponent<Text>().text = info.count.ToString();
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
