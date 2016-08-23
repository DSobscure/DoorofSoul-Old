using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.ElementComponents.Items;
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
        protected ItemInfo[] itemInfos;

        public IEnumerable<ItemInfo> ItemInfos { get { return itemInfos.Where(x => x.item != null).OrderBy(x => x.positionIndex); } }

        private event Action<ItemInfo> onItemChange;
        public event Action<ItemInfo> OnItemChange { add { onItemChange += value; } remove { onItemChange -= value; } }

        public Inventory(int inventoryID, int containerID, int capacity)
        {
            InventoryID = inventoryID;
            ContainerID = containerID;
            Capacity = capacity;
            itemInfos = new ItemInfo[Capacity];
        }

        public void LoadItem(Item item, int count, int positionIndex)
        {
            if(item != null && positionIndex >= 0 && positionIndex < Capacity)
            {
                itemInfos[positionIndex] = new ItemInfo { item = item, count = count, positionIndex = positionIndex };
                onItemChange?.Invoke(itemInfos[positionIndex]);
            }
        }
    }
}
