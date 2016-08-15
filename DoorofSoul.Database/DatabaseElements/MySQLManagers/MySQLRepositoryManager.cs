using DoorofSoul.Database.DatabaseElements.Repositories.MySQL;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers
{
    class MySQLRepositoryManager : RepositoryManager
    {
        public MySQLRepositoryManager()
        {
            PlayerRepository = new MySQLPlayerRepository();
            AnswerRepository = new MySQLAnswerRepository();
            SoulRepository = new MySQLSoulRepository();
            ContainerRepository = new MySQLContainerRepository();
            EntityRepository = new MySQLEntityRepository();
            WorldRepository = new MySQLWorldRepository();
            SceneRepository = new MySQLSceneRepository();

            ItemRepository = new MySQLItemRepository();
            InventoryRepository = new MySQLInventoryRepository();

            AlchemySkillRepository = new MySQLAlchemySkillRepository();
            ElementSkillRepository = new MySQLElementSkillRepository();
            GenieSkillRepository = new MySQLGenieSkillRepository();
            DemonSkillRepository = new MySQLDemonSkillRepository();
            ChanceSkillRepository = new MySQLChanceSkillRepository();
            TechnologySkillRepository = new MySQLTechnologySkillRepository();
            BeliefSkillRepository = new MySQLBeliefSkillRepository();

            SkillInfoRepository = new MySQLSkillInfoRepository();
        }
    }
}
