using System;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.ShadowComponents.UIComponents;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class InventoryItemInfo : IUsableObject
    {
        public int inventoryItemInfoID;
        public Item item;
        public int count;
        public int positionIndex;

        public string Name { get { return item?.ItemName ?? ""; } }
    }
}
