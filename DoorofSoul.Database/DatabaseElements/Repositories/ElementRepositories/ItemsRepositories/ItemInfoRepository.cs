using DoorofSoul.Library.General.ElementComponents.Items;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories
{
    public abstract class ItemInfoRepository
    {
        public abstract void LoadItemInfos(Inventory inventory);
        public abstract void ClearItemInfos(int inventoryID);
        public abstract void InsertItemInfo(int inventoryID, ItemInfo itemInfo);
        public abstract void SaveItemInfos(Inventory inventory);
    }
}
