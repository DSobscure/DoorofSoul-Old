using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public abstract class InventoryRepository
    {
        public abstract Inventory Create(int containerID, int capacity);
        public abstract void Delete(int inventoryID);
        public abstract Inventory Find(int inventoryID);
        public abstract Inventory FindByContainerID(int containerID);
        public abstract void Save(Inventory inventory);
    }
}
