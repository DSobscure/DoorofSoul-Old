using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLSkillsRepositoryList : SkillsRepositoryList
    {
        private MySQLSkillInfoRepository skillInfoRepository;

        public override SkillInfoRepository SkillInfoRepository { get { return skillInfoRepository; } }

        public MySQLSkillsRepositoryList()
        {
            skillInfoRepository = new MySQLSkillInfoRepository();
        }
    }
}
