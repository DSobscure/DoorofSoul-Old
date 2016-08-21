using DoorofSoul.Database.MySQL.DatabaseElements.Connections.ThroneConnections;
using DoorofSoul.Database.DatabaseElements.Connections;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLThroneConnection : ThroneConnection
    {
        public MySQLThroneConnection()
        {
            SoulElementsConnection = new MySQLSoulElementsConnection();
            childConnections.Add(SoulElementsConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Throne";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
