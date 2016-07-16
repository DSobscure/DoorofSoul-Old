using DoorofSoul.Library;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class WorldRepository
    {
        public abstract World Find(int worldID);
        public abstract void Save(World world);
        public abstract List<World> List();
    }
}
