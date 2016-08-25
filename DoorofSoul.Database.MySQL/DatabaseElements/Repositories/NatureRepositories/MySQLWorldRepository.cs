﻿using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.Library;
using DoorofSoul.Library.General.NatureComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    public class MySQLWorldRepository : WorldRepository
    {
        public override WorldData Create(string worldName)
        {
            string sqlString = @"INSERT INTO WorldCollection 
                (WorldName) VALUES (@worldName) ;
                SELECT LAST_INSERT_ID();";
            int worldID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldName", worldName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        worldID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(worldID);
        }

        public override void Delete(int worldID)
        {
            string sqlString = @"DELETE FROM WorldCollection 
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldID", worldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLWorldRepository Delete World Error WorldID: {0}", worldID);
                }
            }
        }

        public override WorldData Find(int worldID)
        {
            string sqlString = @"SELECT  
                WorldName
                from WorldCollection WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
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
                from WorldCollection;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
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
            string sqlString = @"UPDATE WorldCollection SET 
                WorldName = @worldName
                WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@worldName", world.WorldName);
                command.Parameters.AddWithValue("@worldID", world.WorldID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLWorldRepository Save World Error WoridID: {0}", world.WorldID);
                }
            }
        }
    }
}