using DoorofSoul.Library.General.ElementComponents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class Inventory
    {
        public static int GetDefaultCapacity()
        {
            return 40;
        }

        public int InventoryID { get; protected set; }
        public int ContainerID { get; protected set; }
        public int Capacity { get; protected set; }
        public int EmptyBlockCount { get { return itemInfos.Count(x => x.item == null); } }
        protected InventoryItemInfo[] itemInfos;

        public IEnumerable<InventoryItemInfo> ItemInfos { get { return itemInfos.Where(x => x.item != null).OrderBy(x => x.positionIndex); } }

        private event Action<InventoryItemInfo> onItemChange;
        public event Action<InventoryItemInfo> OnItemChange { add { onItemChange += value; } remove { onItemChange -= value; } }

        public Inventory(int inventoryID, int containerID, int capacity)
        {
            InventoryID = inventoryID;
            ContainerID = containerID;
            Capacity = capacity;
            itemInfos = new InventoryItemInfo[Capacity];
            for(int i = 0; i < Capacity; i++)
            {
                itemInfos[i] = new InventoryItemInfo { positionIndex = i };
            }
        }

        public void LoadItem(Item item, int count, int positionIndex)
        {
            if(positionIndex >= 0 && positionIndex < Capacity)
            {
                itemInfos[positionIndex] = new InventoryItemInfo { item = item, count = count, positionIndex = positionIndex };
                onItemChange?.Invoke(itemInfos[positionIndex]);
            }
        }
        public int ItemCount(int itemID)
        {
            return ItemInfos.Where(x => x.item.ItemID == itemID).Sum(x => x.count);
        }
        public bool CanAddItem(Item item, int count)
        {
            if(EmptyBlockCount > 0)
            {
                return true;
            }
            else if(ItemCount(item.ItemID) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddItem(Item item, int count, out InventoryItemInfo newInfo)
        {
            if(CanAddItem(item, count))
            {
                if(ItemCount(item.ItemID) == 0)
                {
                    InventoryItemInfo info = itemInfos.First(x => x.item == null);
                    info.item = item;
                    info.count = count;
                    newInfo = info;
                    LibraryInstance.NatureInterface?.ContainerElementsInterface.InventoryInterface.InsertInventoryItemInfo(InventoryID, info);
                }
                else
                {
                    InventoryItemInfo existItems = ItemInfos.First(x => x.item.ItemID == item.ItemID);
                    existItems.count += count;
                    newInfo = existItems;
                }
                onItemChange?.Invoke(newInfo);
                return true;
            }
            else
            {
                newInfo = null;
                return false;
            }
        }
        public void SwapItemInfo(int originPosition, int newPosition)
        {
            itemInfos[originPosition].positionIndex = newPosition;
            itemInfos[newPosition].positionIndex = originPosition;

            InventoryItemInfo selectedInfo = itemInfos[originPosition];
            itemInfos[originPosition] = itemInfos[newPosition];
            itemInfos[newPosition] = selectedInfo;
            onItemChange?.Invoke(itemInfos[originPosition]);
            onItemChange?.Invoke(itemInfos[newPosition]);
        }
    }
}
