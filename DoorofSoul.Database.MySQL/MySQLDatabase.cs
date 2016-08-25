using DoorofSoul.Database.MySQL.DatabaseElements;
using DoorofSoul.Library.General.LightComponents;
using ExitGames.Logging;

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
