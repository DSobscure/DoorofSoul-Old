using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class WorldRepository
    {
        public abstract DatabaseWorld Create(string worldName);
        public abstract void Delete(int worldID);
        public abstract DatabaseWorld Find(int worldID);
        public abstract void Save(World world);
        public abstract List<DatabaseWorld> List();
    }
}
