using DoorofSoul.Library.General.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Relations
{
    public abstract class InventoryID_ItemInfo_Relations
    {
        public abstract void LoadItemInfos(Inventory inventory);
        public abstract void ClearItemInfos(int inventoryID);
        public abstract void InsertItemInfo(int inventoryID, ItemInfo itemInfo);
        public abstract void SaveItemInfos(Inventory inventory);
    }
}
