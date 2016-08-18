using DoorofSoul.Database.Library;
using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class WorldRepository
    {
        public abstract WorldData Create(string worldName);
        public abstract void Delete(int worldID);
        public abstract WorldData Find(int worldID);
        public abstract void Save(World world);
        public abstract List<WorldData> List();
    }
}
