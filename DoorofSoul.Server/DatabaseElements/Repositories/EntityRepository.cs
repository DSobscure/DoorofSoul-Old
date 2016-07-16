using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class EntityRepository
    {
        public abstract Entity Find(int entityID);
        public abstract void Save(Entity entity);
        public abstract List<Entity> List();
        public abstract List<Entity> ListInScene(int sceneID);
    }
}
