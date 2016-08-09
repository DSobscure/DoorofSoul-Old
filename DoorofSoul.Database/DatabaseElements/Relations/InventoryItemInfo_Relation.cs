using DoorofSoul.Library.General.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Relations
{
    public abstract class InventoryItemInfo_Relation
    {
        public abstract void LoadItemInfos(Inventory inventory);
        public abstract void ClearItemInfos(int inventoryID);
        public abstract void InsertItemInfo(int inventoryID, ItemInfo itemInfo);
        public abstract void SaveItemInfos(Inventory inventory);
    }
}
