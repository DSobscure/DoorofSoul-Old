using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Library.General.NatureComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    public class MySQLSceneRepository : SceneRepository
    {
        public override Scene Create(string sceneName, int worldID)
        {
            string sqlString = @"INSERT INTO SceneCollection 
                (SceneName, WorldID) VALUES (@sceneName, @worldID) ;
                SELECT LAST_INSERT_ID();";
            int sceneID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneName", sceneName);
                command.Parameters.AddWithValue("@worldID", worldID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sceneID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(sceneID);
        }

        public override void Delete(int sceneID)
        {
            string sqlString = @"DELETE FROM SceneCollection 
                WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneID", sceneID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSceneRepository Delete Scene Error SceneID: {0}", sceneID);
                }
            }
        }

        public override Scene Find(int sceneID)
        {
            string sqlString = @"SELECT  
                SceneName, WorldID
                from SceneCollection WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneID", sceneID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string sceneName = reader.GetString(0);
                        int worldID = reader.GetInt32(1);
                        return new Scene(sceneID, sceneName, worldID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Scene> List()
        {
            string sqlString = @"SELECT  
                SceneID, SceneName, WorldID
                from SceneCollection;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Scene> scenes = new List<Scene>();
                    while (reader.Read())
                    {
                        int sceneID = reader.GetInt32(0);
                        string sceneName = reader.GetString(1);
                        int worldID = reader.GetInt32(2);
                        scenes.Add(new Scene(sceneID, sceneName, worldID));
                    }
                    return scenes;
                }
            }
        }

        public override List<Scene> ListOfWorld(int worldID)
        {
            string sqlString = @"SELECT  
                SceneID, SceneName
                from SceneCollection WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("worldID", worldID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Scene> scenes = new List<Scene>();
                    while (reader.Read())
                    {
                        int sceneID = reader.GetInt32(0);
                        string sceneName = reader.GetString(1);
                        scenes.Add(new Scene(sceneID, sceneName, worldID));
                    }
                    return scenes;
                }
            }
        }

        public override void Save(Scene scene)
        {
            string sqlString = @"UPDATE SceneCollection SET 
                SceneName = @sceneName, WorldID = @worldID
                WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneName", scene.SceneName);
                command.Parameters.AddWithValue("@worldID", scene.WorldID);
                command.Parameters.AddWithValue("@sceneID", scene.SceneID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSceneRepository Save Scene Error SceneID: {0}", scene.SceneID);
                }
            }
        }
    }
}
