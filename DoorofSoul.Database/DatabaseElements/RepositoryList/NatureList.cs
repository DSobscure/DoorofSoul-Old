using DoorofSoul.Database.DatabaseElements.Repositories.Nature;
using DoorofSoul.Database.DatabaseElements.RepositoryList.Nature;

namespace DoorofSoul.Database.DatabaseElements.RepositoryList
{
    public class NatureList
    {
        public ContainerRepository ContainerRepository { get; protected set; }
        public EntityRepository EntityRepository { get; protected set; }
        public WorldRepository WorldRepository { get; protected set; }
        public SceneRepository SceneRepository { get; protected set; }

        public ContainerElementsList ContainerElementsList { get; protected set; }
        public EntityElementsList EntityElementsList { get; protected set; }
    }
}
