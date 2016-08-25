using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLStatusEffectsRepositoryList : StatusEffectsRepositoryList
    {
        private MySQLContainerStatusEffectInfoRepository containerStatusEffectInfoRepository;

        public override ContainerStatusEffectInfoRepository ContainerStatusEffectInfoRepository { get { return containerStatusEffectInfoRepository; } }

        public MySQLStatusEffectsRepositoryList()
        {
            containerStatusEffectInfoRepository = new MySQLContainerStatusEffectInfoRepository();
        }
    }
}
