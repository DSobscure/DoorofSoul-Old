using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLMindRepositoryList : MindRepositoryList
    {
        private MySQLSoulRepository soulRepository;
        private MySQLSoulElementsRepositoryList soulElementsRepositoryList;

        public override SoulRepository SoulRepository { get { return soulRepository; } }
        public override SoulElementsRepositoryList SoulElementsRepositoryList { get { return soulElementsRepositoryList; } }

        public MySQLMindRepositoryList()
        {
            soulRepository = new MySQLSoulRepository();

            soulElementsRepositoryList = new MySQLSoulElementsRepositoryList();
        }
    }
}
