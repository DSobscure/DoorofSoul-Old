using DoorofSoul.Database.DatabaseElements.Repositories.Nature;
using DoorofSoul.Database.Library;
using DoorofSoul.Library.General.NatureComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature
{
    public class MySQLWorldRepository : WorldRepository
    {
        public override WorldData Create(string worldName)
        {
            string sqlString = @"INSERT INTO Nature_Worlds 
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
            string sqlString = @"DELETE FROM Nature_Worlds 
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldID", worldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLWorldRepository Delete World Error WorldID: {0}", worldID);
                }
            }
        }

        public override WorldData Find(int worldID)
        {
            string sqlString = @"SELECT  
                WorldName
                from Nature_Worlds WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldID", worldID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string worldName = reader.GetString(0);
                        return new WorldData { worldID = worldID, worldName = worldName };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<WorldData> List()
        {
            string sqlString = @"SELECT  
                WorldID, WorldName
                from Nature_Worlds;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<WorldData> worlds = new List<WorldData>();
                    while (reader.Read())
                    {
                        int worldID = reader.GetInt32(0);
                        string worldName = reader.GetString(1);
                        worlds.Add(new WorldData { worldID = worldID, worldName = worldName });
                    }
                    return worlds;
                }
            }
        }

        public override void Save(World world)
        {
            string sqlString = @"UPDATE Nature_Worlds SET 
                WorldName = @worldName
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldName", world.WorldName);
                command.Parameters.AddWithValue("@worldID", world.WorldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLWorldRepository Save World Error WoridID: {0}", world.WorldID);
                }
            }
        }
    }
}
