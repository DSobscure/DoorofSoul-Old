using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public abstract class InventoryItemInfoRepository
    {
        public abstract void LoadInventoryItemInfos(Inventory inventory);
        public abstract void ClearInventoryItemInfos(int inventoryID);
        public abstract void InsertInventoryItemInfo(int inventoryID, InventoryItemInfo inventoryItemInfo);
        public abstract void DeleteInventoryItemInfo(int inventoryItemInfoID);
        public abstract void SaveInventoryItemInfo(InventoryItemInfo inventoryItemInfo, int inventoryID);
        public abstract void SaveInventoryItemInfos(Inventory inventory);
    }
}
