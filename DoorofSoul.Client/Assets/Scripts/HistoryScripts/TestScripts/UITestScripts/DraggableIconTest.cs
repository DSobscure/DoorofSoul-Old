using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Client.Scripts.ShadowScripts.UiScripts.IconScripts;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UITestScripts
{
    public class DraggableIconTest : MonoBehaviour
    {
        [SerializeField]
        private DraggableIcon icon1;
        [SerializeField]
        private DraggableIcon icon2;

        void Start()
        {
            icon1.Initial(new InventoryItemInfo { item = new Item(1, "物品1", "") });
            icon2.Initial(new InventoryItemInfo { item = new Item(2, "物品2", "") });
        }
    }
}
