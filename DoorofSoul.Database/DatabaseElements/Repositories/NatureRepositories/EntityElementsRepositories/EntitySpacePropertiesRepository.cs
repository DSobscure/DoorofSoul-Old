using DoorofSoul.Library.General.NatureComponents.EntityElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.EntityElementsRepositories
{
    public abstract class EntitySpacePropertiesRepository
    {
        public abstract EntitySpaceProperties Create(int entityID, EntitySpaceProperties spaceProperties);
        public abstract void Delete(int entityID);
        public abstract EntitySpaceProperties Find(int entityID);
        public abstract void Save(int entityID, EntitySpaceProperties entitySpaceProperties);
    }
}
