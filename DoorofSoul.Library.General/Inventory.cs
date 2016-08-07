using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public struct ItemInfo
    {
        public Item item;
        public int count;
        public int positionIndex;
    }

    public class Inventory
    {
        public int InventoryID { get; protected set; }
        public int ContainerID { get; protected set; }
        public int Capacity { get; protected set; }
        protected ItemInfo[] itemInfos;
        public IEnumerable<ItemInfo> ItemInfos { get { return itemInfos.Where(x => x.item != null).OrderBy(x => x.positionIndex); } }

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
            }
        }
    }
}
