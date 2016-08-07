﻿using System.IO;
using MsgPack.Serialization;

namespace DoorofSoul.Library.General
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

        public static byte[] Serialize(object customType)
        {
            Item item = customType as Item;
            var serializer = MessagePackSerializer.Get<Item>();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Pack(memoryStream, item);
                return memoryStream.ToArray();
            }
        }

        public int ItemID { get; protected set; }
        public string ItemName { get; protected set; }
        public string Description { get; protected set; }

        [MessagePackDeserializationConstructor]
        public Item() { }
        public Item(int itemID, string itemName, string description)
        {
            ItemID = itemID;
            ItemName = itemName;
            Description = description;
        }
    }
}
