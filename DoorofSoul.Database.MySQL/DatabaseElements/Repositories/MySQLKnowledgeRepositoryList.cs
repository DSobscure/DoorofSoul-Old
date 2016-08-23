using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLKnowledgeRepositoryList : KnowledgeRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            SkillRepository = new MySQLSkillRepository();
            StatusEffectRepository = new MySQLStatusEffectRepository();

            SkillsRepositoryList = new MySQLSkillsRepositoryList();
            StatusEffectsRepositoryList = new MySQLStatusEffectsRepositoryList();
        }
    }
}
