using DoorofSoul.Database.DatabaseElements.Authentications.MySQL;
using DoorofSoul.Database.DatabaseElements.Relations.MySQL;
using DoorofSoul.Database.DatabaseElements.Repositories.MySQL;
using MySql.Data.MySqlClient;
using ExitGames.Logging;

namespace DoorofSoul.Database
{
    public class MySQLDatabase : DataBase
    {
        public MySQLDatabase(ILogger log) : base(log)
        {
            AuthenticationManager.PlayerAuthentication = new MySQLPlayerAuthentication();
            RepositoryManager.PlayerRepository = new MySQLPlayerRepository();
            RepositoryManager.AnswerRepository = new MySQLAnswerRepository();
            RepositoryManager.SoulRepository = new MySQLSoulRepository();
            RepositoryManager.ContainerRepository = new MySQLContainerRepository();
            RepositoryManager.EntityRepository = new MySQLEntityRepository();
            RepositoryManager.WorldRepository = new MySQLWorldRepository();
            RepositoryManager.SceneRepository = new MySQLSceneRepository();

            RelationManager.SoulID_ContainerID_Relation = new MySQL_SoulID_ContainerID_Relation();
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}", hostName, userName, password, database);
            connection = new MySqlConnection(connectString);
            return connection != null;
        }
    }
}
