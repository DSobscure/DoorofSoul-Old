using DoorofSoul.Database.DatabaseElements.Repositories;

namespace DoorofSoul.Database.DatabaseElements
{
    public class RepositoryManager
    {
        public PlayerRepository PlayerRepository { get; set; }
        public AnswerRepository AnswerRepository { get; set; }
        public SoulRepository SoulRepository { get; set; }
        public ContainerRepository ContainerRepository { get; set; }
        public EntityRepository EntityRepository { get; set; }
        public WorldRepository WorldRepository { get; set; }
        public SceneRepository SceneRepository { get; set; }
    }
}
