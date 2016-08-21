using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class NatureRepositoryList : InstantiableRepositoryList
    {
        public WorldRepository WorldRepository { get; protected set; }
        public SceneRepository SceneRepository { get; protected set; }
        public ContainerRepository ContainerRepository { get; protected set; }
        public EntityRepository EntityRepository { get; protected set; }

        public ContainerElementsRepositoryList ContainerElementsRepositoryList { get; protected set; }
        public EntityElementsRepositoryList EntityElementsRepositoryList { get; protected set; }
    }
}
