using System;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces;

namespace DoorofSoul.Hexagram.LightComponents
{
    class HexagramElementInterface : ElementInterface
    {
        public Item FindItem(int itemID)
        {
            return Hexagram.Element.ItemManager.FindItem(itemID);
        }
    }
}
