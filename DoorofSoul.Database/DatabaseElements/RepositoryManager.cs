using DoorofSoul.Database.DatabaseElements.Repositories;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RepositoryManager
    {
        public PlayerRepository PlayerRepository { get; protected set; }
        public AnswerRepository AnswerRepository { get; protected set; }
        public SoulRepository SoulRepository { get; protected set; }
        public ContainerRepository ContainerRepository { get; protected set; }
        public EntityRepository EntityRepository { get; protected set; }
        public WorldRepository WorldRepository { get; protected set; }
        public SceneRepository SceneRepository { get; protected set; }

        public ItemRepository ItemRepository { get; protected set; }
        public InventoryRepository InventoryRepository { get; protected set; }

        public AlchemySkillRepository AlchemySkillRepository { get; protected set; }
        public ElementSkillRepository ElementSkillRepository { get; protected set; }
        public GenieSkillRepository GenieSkillRepository { get; protected set; }
        public DemonSkillRepository DemonSkillRepository { get; protected set; }
        public ChanceSkillRepository ChanceSkillRepository { get; protected set; }
        public TechnologySkillRepository TechnologySkillRepository { get; protected set; }
        public BeliefSkillRepository BeliefSkillRepository { get; protected set; }

        public SkillInfoRepository SkillInfoRepository { get; protected set; }
    }
}
