using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLStatusEffectsRepositoryList : StatusEffectsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            ContainerStatusEffectInfoRepository = new MySQLContainerStatusEffectInfoRepository();
        }
    }
}
