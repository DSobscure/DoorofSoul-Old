using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Library.General.LightComponents.Nature.ContainerElements
{
    public interface InventoryInterface
    {
        void InsertInventoryItemInfo(int inventoryID, InventoryItemInfo info);
        void DeleteInventoryItemInfo(int inventoryItemInfoID);
    }
}
