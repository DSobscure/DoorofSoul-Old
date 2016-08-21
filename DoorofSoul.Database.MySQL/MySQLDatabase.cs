using DoorofSoul.Database.MySQL.DatabaseElements;
using MySql.Data.MySqlClient;
using ExitGames.Logging;
using DoorofSoul.Database.DatabaseElements.Connections;

namespace DoorofSoul.Database.MySQL
{
    public class MySQLDatabase : Database
    {
        public MySQLDatabase(ILogger log, KnowledgeInterface knowledgeInterface) : base(log, knowledgeInterface)
        {
            connectionList = new MySQLConnectionList();
            repositoryList = new MySQLRepositoryList();
        }
    }
}
