using DoorofSoul.Database.DatabaseElements.Connections;
using DoorofSoul.Database.DatabaseElements.Connections.MindConnections;
using DoorofSoul.Database.MySQL.DatabaseElements.Connections.MindConnections;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLMindConnection : MindConnection
    {
        private MySQLSoulElementsConnection soulElementsConnection;
        public override SoulElementsConnection SoulElementsConnection { get { return soulElementsConnection; } }

        public MySQLMindConnection()
        {
            soulElementsConnection = new MySQLSoulElementsConnection();
            childConnections.Add(soulElementsConnection);
        }
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Mind";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
