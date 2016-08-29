using System;
using DoorofSoul.Database.DatabaseElements.Connections;
using DoorofSoul.Database.MySQL.DatabaseElements.Connections.LightConnections;
using DoorofSoul.Database.DatabaseElements.Connections.LightConnections;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections
{
    class MySQLLightConnection : LightConnection
    {
        private MySQLEffectsConnection effectsConnection;
        public override EffectsConnection EffectsConnection { get { return effectsConnection; } }

        public MySQLLightConnection()
        {
            effectsConnection = new MySQLEffectsConnection();

            childConnections.Add(effectsConnection);
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Light";

            childConnections.ForEach(x => x.Connect(hostName, userName, password, string.Format("{0}_{1}", database, databaseName)));

            return Connected;
        }
    }
}
