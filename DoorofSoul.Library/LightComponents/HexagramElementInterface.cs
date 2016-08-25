using System;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.LightComponents;

namespace DoorofSoul.Hexagram.LightComponents
{
    class HexagramElementInterface : ElementInterface
    {
        public Item FindItem(int itemID)
        {
            return Hexagram.Instance.Element.ItemManager.FindItem(itemID);
        }
    }
}
