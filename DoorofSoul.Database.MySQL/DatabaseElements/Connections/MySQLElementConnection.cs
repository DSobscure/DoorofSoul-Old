using DoorofSoul.Database.MySQL.DatabaseElements.Connections.ElementConnections;
using DoorofSoul.Database.DatabaseElements.Connections;
using MySql.Data.MySqlClient;
using DoorofSoul.Database.DatabaseElements.Connections.ElementConnections;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLElementConnection : ElementConnection
    {
        private MySQLItemsConnection itemsConnection;
        public override ItemsConnection ItemsConnection { get { return itemsConnection; } }

        public MySQLElementConnection()
        {
            itemsConnection = new MySQLItemsConnection();
            childConnections.Add(itemsConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Element";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
