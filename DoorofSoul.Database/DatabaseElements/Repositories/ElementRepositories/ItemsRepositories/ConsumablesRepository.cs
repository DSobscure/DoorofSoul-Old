using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.ElementComponents.Items;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories
{
    public abstract class ConsumablesRepository
    {
        public abstract Consumables Create(int itemID);
        public abstract void Delete(int consumablesID);
        public abstract Consumables Find(int consumablesID);
        public abstract void Save(Consumables consumables, int itemID);
        public abstract void AssemblyConsumables(Item item);
    }
}
