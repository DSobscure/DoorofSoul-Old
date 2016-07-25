﻿using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLWorldRepository : WorldRepository
    {
        public override DatabaseWorld Create(string worldName)
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
                    DataBase.Instance.Log.ErrorFormat("MySQLWorldRepository Delete World Error WorldID: {0}", worldID);
                }
            }
        }

        public override DatabaseWorld Find(int worldID)
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
                        return new DatabaseWorld(worldID, worldName);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<DatabaseWorld> List()
        {
            string sqlString = @"SELECT  
                WorldID, WorldName
                from Worlds;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<DatabaseWorld> worlds = new List<DatabaseWorld>();
                    while (reader.Read())
                    {
                        int worldID = reader.GetInt32(0);
                        string worldName = reader.GetString(1);
                        worlds.Add(new DatabaseWorld(worldID, worldName));
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
                    DataBase.Instance.Log.ErrorFormat("MySQLWorldRepository Save World Error WoridID: {0}", world.WorldID);
                }
            }
        }
    }
}
