using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Knowledge.Skills;
using DoorofSoul.Database.DatabaseElements.RepositoryList.Knowledge;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList.Knowledge
{
    public class MySQLSkillsList : SkillsList
    {
        public MySQLSkillsList()
        {
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
