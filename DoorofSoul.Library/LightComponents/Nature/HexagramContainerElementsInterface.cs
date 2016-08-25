using DoorofSoul.Hexagram.LightComponents.Nature.ContainerElements;
using DoorofSoul.Library.General.LightComponents.Nature;
using DoorofSoul.Library.General.LightComponents.Nature.ContainerElements;

namespace DoorofSoul.Hexagram.LightComponents.Nature
{
    class HexagramContainerElementsInterface : ContainerElementsInterface
    {
        private HexagramInventoryInterface inventoryInterface;
        public InventoryInterface InventoryInterface
        {
            get
            {
                return inventoryInterface;
            }
        }

        public HexagramContainerElementsInterface()
        {
            inventoryInterface = new HexagramInventoryInterface();
        }
    }
}
