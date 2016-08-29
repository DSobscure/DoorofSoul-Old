using DoorofSoul.Database.DatabaseElements.Connections.Path;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections.Path
{
    class MySQLSeperation_ConcretionConnection : Seperation_ConcretionConnection
    {
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Seperation_Concretion";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            return Connected;
        }
    }
}
