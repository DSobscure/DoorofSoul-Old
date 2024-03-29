﻿using MySql.Data.MySqlClient;
using DoorofSoul.Database.DatabaseElements.Connections.LightConnections;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections.LightConnections
{
    class MySQLEffectsConnection : EffectsConnection
    {
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "Effects";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);
            return Connected;
        }
    }
}
