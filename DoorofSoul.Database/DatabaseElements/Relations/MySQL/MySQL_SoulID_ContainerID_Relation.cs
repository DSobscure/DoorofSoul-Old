using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Relations.MySQL
{
    public class MySQL_SoulID_ContainerID_Relation : SoulID_ContainerID_Relations
    {
        public override List<int> GetContainerIDs(int soulID)
        {
            string sqlString = @"SELECT  
                ContainerID
                from SoulID_ContainerID_Relations WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
                from SoulID_ContainerID_Relations WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
            string sqlString = @"INSERT INTO SoulID_ContainerID_Relations 
                (SoulID, ContainerID) VALUES (@soulID, @containerID) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQL_SoulID_ContainerID_Relation Link_Soul_Container Error SoulID: {0}, ContainerID: {1}", soulID, containerID);
                }
            }
        }

        public override void Unlink_Soul_Container(int soulID, int containerID)
        {
            string sqlString = @"DELETE FROM SoulID_ContainerID_Relations 
                WHERE SoulID = @soulID AND ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQL_SoulID_ContainerID_Relation Unlink_Soul_Container Error SoulID: {0}, ContainerID: {1}", soulID, containerID);
                }
            }
        }
    }
}
