using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class EntityRepository
    {
        public abstract Entity Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties);
        public abstract void Delete(int entityID);
        public abstract Entity Find(int entityID);
        public abstract void Save(Entity entity);
        public abstract List<Entity> List();
        public abstract List<Entity> ListInScene(int sceneID);
    }
}
