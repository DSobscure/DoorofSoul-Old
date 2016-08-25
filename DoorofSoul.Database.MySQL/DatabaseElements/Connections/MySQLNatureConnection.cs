using DoorofSoul.Database.MySQL.DatabaseElements.Connections.NatureConnections;
using DoorofSoul.Database.DatabaseElements.Connections;
using MySql.Data.MySqlClient;
using DoorofSoul.Database.DatabaseElements.Connections.NatureConnections;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLNatureConnection : NatureConnection
    {
        private MySQLContainerElementsConnection containerElementsConnection;
        private MySQLEntityElementsConnection entityElementsConnection;
        private MySQLSceneElementsConnection sceneElementsConnection;

        public override ContainerElementsConnection ContainerElementsConnection { get { return containerElementsConnection; } }
        public override EntityElementsConnection EntityElementsConnection { get { return entityElementsConnection; } }
        public override SceneElementsConnection SceneElementsConnection { get { return sceneElementsConnection; } }

        public MySQLNatureConnection()
        {
            containerElementsConnection = new MySQLContainerElementsConnection();
            entityElementsConnection = new MySQLEntityElementsConnection();
            sceneElementsConnection = new MySQLSceneElementsConnection();

            childConnections.Add(containerElementsConnection);
            childConnections.Add(entityElementsConnection);
            childConnections.Add(sceneElementsConnection);
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
