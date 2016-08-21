using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.EntityElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories
{
    public abstract class EntityElementsRepositoryList : InstantiableRepositoryList
    {
        public EntitySpacePropertiesRepository EntitySpacePropertiesRepository { get; protected set; }
    }
}
