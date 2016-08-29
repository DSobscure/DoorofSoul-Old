using DoorofSoul.Database.MySQL.DatabaseElements.Connections;
using DoorofSoul.Database.DatabaseElements;
using MySql.Data.MySqlClient;
using DoorofSoul.Database.DatabaseElements.Connections;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements
{
    class MySQLConnectionList : ConnectionList
    {
        private MySQLKnowledgeConnection knowledgeConnection;
        private MySQLElementConnection elementConnection;
        private MySQLLightConnection lightConnection;
        private MySQLLoveConnection loveConnection;
        private MySQLNatureConnection natureConnection;
        private MySQLMindConnection mindConnection;
        private MySQLThroneConnection throneConnection;

        private MySQLPathConnectionList pathConnectionList;

        public override KnowledgeConnection KnowledgeConnection { get { return knowledgeConnection; } }
        public override ElementConnection ElementConnection { get { return elementConnection; } }
        public override LightConnection LightConnection { get { return lightConnection; } }
        public override LoveConnection LoveConnection { get { return loveConnection; } }
        public override NatureConnection NatureConnection { get { return natureConnection; } }
        public override MindConnection MindConnection { get { return mindConnection; } }
        public override ThroneConnection ThroneConnection { get { return throneConnection; } }

        public override PathConnectionList PathConnectionList { get { return pathConnectionList; } }

        public MySQLConnectionList()
        {
            knowledgeConnection = new MySQLKnowledgeConnection();
            elementConnection = new MySQLElementConnection();
            lightConnection = new MySQLLightConnection();
            loveConnection = new MySQLLoveConnection();
            natureConnection = new MySQLNatureConnection();
            mindConnection = new MySQLMindConnection();
            throneConnection = new MySQLThroneConnection();

            pathConnectionList = new MySQLPathConnectionList();

            childConnections.Add(knowledgeConnection);
            childConnections.Add(elementConnection);
            childConnections.Add(lightConnection);
            childConnections.Add(loveConnection);
            childConnections.Add(natureConnection);
            childConnections.Add(mindConnection);
            childConnections.Add(throneConnection);

            childConnections.Add(pathConnectionList);
        }
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}", hostName, userName, password, database);
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, database));

            return Connected;
        }
    }
}
