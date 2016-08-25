using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class NatureRepositoryList
    {
        public abstract WorldRepository WorldRepository { get; }
        public abstract SceneRepository SceneRepository { get; }
        public abstract ContainerRepository ContainerRepository { get; }
        public abstract EntityRepository EntityRepository { get; }

        public abstract ContainerElementsRepositoryList ContainerElementsRepositoryList { get; }
        public abstract EntityElementsRepositoryList EntityElementsRepositoryList { get; }
        public abstract SceneElementsRepositoryList SceneElementsRepositoryList { get; }
    }
}
