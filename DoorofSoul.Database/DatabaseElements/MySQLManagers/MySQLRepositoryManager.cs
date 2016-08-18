using DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList;
using DoorofSoul.Database.DatabaseElements.Repositories.MySQL;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers
{
    class MySQLRepositoryManager : RepositoryManager
    {
        public MySQLRepositoryManager()
        {
            PlayerRepository = new MySQLPlayerRepository();
            KnowledgeList = new MySQLKnowledgeList();
            NatureList = new MySQLNatureList();
            ThroneList = new MySQLThroneList();
        }
    }
}
