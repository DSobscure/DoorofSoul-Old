using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.Path;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLPathRepositoryList : PathRepositoryList
    {
        private MySQLEgo_CognitionRepositoryList ego_CognitionRepositoryList;
        public override Ego_CognitionRepositoryList Ego_CognitionRepositoryList { get { return ego_CognitionRepositoryList; } }

        public MySQLPathRepositoryList()
        {
            ego_CognitionRepositoryList = new MySQLEgo_CognitionRepositoryList();
        }
    }
}
