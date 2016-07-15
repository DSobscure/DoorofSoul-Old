using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Server;
using DoorofSoul.Server.DatabaseElements.Authentications.MySQL;
using DoorofSoul.Server.DatabaseElements.Repositories.MySQL;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Server.Databases
{
    public class MySQLDatabase : DataBase
    {
        public MySQLDatabase()
        {
            AuthenticationManager.PlayerAuthentication = new MySQLPlayerAuthentication();
            RepositoryManager.PlayerRepository = new MySQLPlayerRepository();
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}", hostName, userName, password, database);
            connection = new MySqlConnection(connectString);
            return connection != null;
        }
    }
}
