using DoorofSoul.Library.General.LightComponents.Nature.ContainerElements;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Hexagram.LightComponents.Nature.ContainerElements
{
    class HexagramInventoryInterface : InventoryInterface
    {
        public void DeleteInventoryItemInfo(int inventoryItemInfoID)
        {
            Database.Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.InventoryItemInfoRepository.DeleteInventoryItemInfo(inventoryItemInfoID);
        }

        public void InsertInventoryItemInfo(int inventoryID, InventoryItemInfo info)
        {
            Database.Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.InventoryItemInfoRepository.InsertInventoryItemInfo(inventoryID, info);
        }
    }
}
