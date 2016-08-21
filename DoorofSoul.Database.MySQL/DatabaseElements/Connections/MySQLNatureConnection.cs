using DoorofSoul.Database.MySQL.DatabaseElements.Connections.NatureConnections;
using DoorofSoul.Database.DatabaseElements.Connections;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLNatureConnection : NatureConnection
    {
        public MySQLNatureConnection()
        {
            ContainerElementsConnection = new MySQLContainerElementsConnection();
            EntityElementsConnection = new MySQLEntityElementsConnection();
            childConnections.Add(ContainerElementsConnection);
            childConnections.Add(EntityElementsConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Nature";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
