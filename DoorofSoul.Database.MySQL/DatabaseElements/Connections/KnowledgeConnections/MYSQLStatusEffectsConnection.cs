﻿using DoorofSoul.Database.DatabaseElements.Connections.KnowledgeConnections;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Connections.KnowledgeConnections
{
    class MySQLStatusEffectsConnection : StatusEffectsConnection
    {
        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string databaseName = "StatusEffects";
            string connectString = string.Format("server={0};uid={1};pwd={2};database={3}_{4}", hostName, userName, password, database, databaseName);
            connection = new MySqlConnection(connectString);
            return Connected;
        }
    }
}
