using DoorofSoul.Database.DatabaseElements.MySQLManagers;
using MySql.Data.MySqlClient;
using ExitGames.Logging;

namespace DoorofSoul.Database
{
    public class MySQLDatabase : DataBase
    {
        public MySQLDatabase(ILogger log, KnowledgeInterface knowledgeInterface) : base(log, knowledgeInterface)
        {
            AuthenticationManager = new MySQLAuthenticationManager();
            RepositoryManager = new MySQLRepositoryManager();
            RelationManager = new MySQLRelationManager();
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}", hostName, userName, password, database);
            connection = new MySqlConnection(connectString);
            return connection != null;
        }
    }
}
