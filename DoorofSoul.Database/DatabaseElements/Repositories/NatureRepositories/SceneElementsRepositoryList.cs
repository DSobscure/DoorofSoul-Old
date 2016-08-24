using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories
{
    public abstract class SceneElementsRepositoryList : InstantiableRepositoryList
    {
        public ItemEntityRepository ItemEntityRepository { get; protected set; }
    }
}
