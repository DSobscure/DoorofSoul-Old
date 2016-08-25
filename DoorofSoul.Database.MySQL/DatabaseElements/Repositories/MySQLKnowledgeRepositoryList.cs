using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLKnowledgeRepositoryList : KnowledgeRepositoryList
    {
        private MySQLSkillRepository skillRepository;
        private MySQLStatusEffectRepository statusEffectRepository;

        private MySQLSkillsRepositoryList skillsRepositoryList;
        private MySQLStatusEffectsRepositoryList statusEffectsRepositoryList;

        public override SkillRepository SkillRepository { get { return skillRepository; } }
        public override StatusEffectRepository StatusEffectRepository { get { return statusEffectRepository; } }

        public override SkillsRepositoryList SkillsRepositoryList { get { return skillsRepositoryList; } }
        public override StatusEffectsRepositoryList StatusEffectsRepositoryList { get { return statusEffectsRepositoryList; } }

        public MySQLKnowledgeRepositoryList()
        {
            skillRepository = new MySQLSkillRepository();
            statusEffectRepository = new MySQLStatusEffectRepository();

            skillsRepositoryList = new MySQLSkillsRepositoryList();
            statusEffectsRepositoryList = new MySQLStatusEffectsRepositoryList();
        }
    }
}
