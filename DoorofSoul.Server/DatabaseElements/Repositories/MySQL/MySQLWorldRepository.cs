using DoorofSoul.Library;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLWorldRepository : WorldRepository
    {
        public override World Create(string worldName)
        {
            string sqlString = @"INSERT INTO Worlds 
                (WorldName) VALUES (@worldName) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldName", worldName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int worldID = reader.GetInt32(0);
                        return Find(worldID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int worldID)
        {
            string sqlString = @"DELETE FROM Worlds 
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldID", worldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Application.Log.ErrorFormat("MySQLWorldRepository Delete World Error WorldID: {0}", worldID);
                }
            }
        }

        public override World Find(int worldID)
        {
            string sqlString = @"SELECT  
                WorldName
                from Worlds WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldID", worldID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string worldName = reader.GetString(0);
                        return new World(worldID, worldName);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<World> List()
        {
            string sqlString = @"SELECT  
                WorldID, WorldName
                from Worlds;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<World> worlds = new List<World>();
                    while(reader.Read())
                    {
                        int worldID = reader.GetInt32(0);
                        string worldName = reader.GetString(1);
                        worlds.Add(new World(worldID, worldName));
                    }
                    return worlds;
                }
            }
        }

        public override void Save(World world)
        {
            string sqlString = @"UPDATE Worlds SET 
                WorldName = @worldName
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldName", world.WorldName);
                command.Parameters.AddWithValue("@worldID", world.WorldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Application.Log.ErrorFormat("MySQLWorldRepository Save World Error WoridID: {0}", world.WorldID);
                }
            }
        }
    }
}
