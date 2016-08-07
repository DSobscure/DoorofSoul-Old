using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ItemRepository
    {
        public abstract Item Create(string itemName, string description);
        public abstract void Delete(int itemID);
        public abstract Item Find(int itemID);
        public abstract void Save(Item item);
    }
}
