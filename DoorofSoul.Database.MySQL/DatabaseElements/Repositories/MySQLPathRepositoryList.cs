using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.Path;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLPathRepositoryList : PathRepositoryList
    {
        private MySQLSeperation_ConcretionRepositoryList seperation_ConcretionRepositoryList;
        private MySQLEgo_CognitionRepositoryList ego_CognitionRepositoryList;

        public override Seperation_ConcretionRepositoryList Seperation_ConcretionRepositoryList { get { return seperation_ConcretionRepositoryList; } }
        public override Ego_CognitionRepositoryList Ego_CognitionRepositoryList { get { return ego_CognitionRepositoryList; } }
        

        public MySQLPathRepositoryList()
        {
            seperation_ConcretionRepositoryList = new MySQLSeperation_ConcretionRepositoryList();
            ego_CognitionRepositoryList = new MySQLEgo_CognitionRepositoryList();
        }
    }
}
