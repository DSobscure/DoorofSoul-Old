using DoorofSoul.Database.DatabaseElements.RepositoryList;
using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Throne;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList
{
    public class MySQLThroneList : ThroneList
    {
        public MySQLThroneList()
        {
            AnswerRepository = new MySQLAnswerRepository();
            SoulRepository = new MySQLSoulRepository();
        }
    }
}
