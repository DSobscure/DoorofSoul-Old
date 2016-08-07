using System;
using DoorofSoul.Library.General;
namespace DoorofSoul.Database.DatabaseElements.Repositories
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
