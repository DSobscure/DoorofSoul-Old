using DoorofSoul.Library.General.ElementComponents;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.ElementComponents
{
    public class ItemManager
    {
        protected Dictionary<int, Item> itemDictionary;


        public ItemManager()
        {
            itemDictionary = new Dictionary<int, Item>();
            LoadItems(Database.Database.RepositoryList.ElementRepositoryList.ItemRepository.List());
        }
        public bool ContainsItem(int itemID)
        {
            return itemDictionary.ContainsKey(itemID);
        }
        public Item FindItem(int itemID)
        {
            if (ContainsItem(itemID))
            {
                return itemDictionary[itemID];
            }
            else
            {
                return null;
            }
        }
        public void LoadItem(Item item)
        {
            if (!ContainsItem(item.ItemID))
            {
                itemDictionary.Add(item.ItemID, item);
            }
        }
        public void LoadItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                if (!ContainsItem(item.ItemID))
                {
                    itemDictionary.Add(item.ItemID, item);
                }
            }
        }
    }
}
