using System;
using System.Collections.Generic;
using DoorofSoul.Database.DatabaseElements.Repositories.Path.Ego_CognitionRepositories;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path.Ego_CognitionRepositories
{
    class MySQLSoulContainerLinkRepository : SoulContainerLinkRepository
    {
        public override List<int> GetContainerIDs(int soulID)
        {
            string sqlString = @"SELECT  
                ContainerID
                from SoulContainerLinkCollection WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Ego_CognitionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<int> containerIDs = new List<int>();
                    while (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        containerIDs.Add(containerID);
                    }
                    return containerIDs;
                }
            }
        }

        public override List<int> GetSoulIDs(int containerID)
        {
            string sqlString = @"SELECT  
                SoulID
                from SoulContainerLinkCollection WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Ego_CognitionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<int> soulIDs = new List<int>();
                    while (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        soulIDs.Add(containerID);
                    }
                    return soulIDs;
                }
            }
        }

        public override void Link_Soul_Container(int soulID, int containerID)
        {
            string sqlString = @"INSERT INTO SoulContainerLinkCollection 
                (SoulID, ContainerID) VALUES (@soulID, @containerID) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Ego_CognitionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulContainerLinkRepository Link_Soul_Container Error SoulID: {0}, ContainerID: {1}", soulID, containerID);
                }
            }
        }

        public override void Unlink_Soul_Container(int soulID, int containerID)
        {
            string sqlString = @"DELETE FROM SoulContainerLinkCollection 
                WHERE SoulID = @soulID AND ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Ego_CognitionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulContainerLinkRepository Unlink_Soul_Container Error SoulID: {0}, ContainerID: {1}", soulID, containerID);
                }
            }
        }
    }
}
