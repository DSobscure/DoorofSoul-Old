using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLSkillsRepositoryList : SkillsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            SkillInfoRepository = new MySQLSkillInfoRepository();
        }
    }
}
