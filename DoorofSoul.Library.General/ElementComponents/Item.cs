using DoorofSoul.Library.General.ElementComponents.Items;
using DoorofSoul.Library.General.LightComponents.Effects;
using DoorofSoul.Protocol;
using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoorofSoul.Library.General.ElementComponents
{
    public class Item
    {
        public static object Deserialize(byte[] data)
        {
            var serializer = MessagePackSerializer.Get<Item>();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return serializer.Unpack(ms);
            }
        }

        public static byte[] Serialize(object data)
        {
            Item value = data as Item;
            var serializer = MessagePackSerializer.Get<Item>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        public int ItemID { get; protected set; }
        public string ItemName { get; protected set; }
        public string Description { get; protected set; }
        protected List<ItemComponent> components;

        [MessagePackDeserializationConstructor]
        public Item()
        {
            components = new List<ItemComponent>();
        }
        public Item(int itemID, string itemName, string description)
        {
            ItemID = itemID;
            ItemName = itemName;
            Description = description;
            components = new List<ItemComponent>();
        }
        public bool Use(ItemComponentTypeCode useType, List<IEffectorTarget> targets)
        {
            if(components.Count != 0)
            {
                return components.Where(x => x.ItemComponentTypeCode == useType).All(x => x.Use(targets));
            }
            else
            {
                return false;
            }
        }
        public void AddComponent(ItemComponent component)
        {
            components.Add(component);
        }
        public void RemoveComponent(ItemComponent component)
        {
            components.Remove(component);
        }
        public void LoadComponents(IEnumerable<ItemComponent> components)
        {
            this.components.AddRange(components);
        }
        public void ClearComponents()
        {
            components.Clear();
        }
    }
}
