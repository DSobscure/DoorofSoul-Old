using DoorofSoul.Library.General.NatureComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLSceneRepository : SceneRepository
    {
        public override Scene Create(string sceneName, int worldID)
        {
            string sqlString = @"INSERT INTO Scenes 
                (SceneName, WorldID) VALUES (@sceneName, @worldID) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneName", sceneName);
                command.Parameters.AddWithValue("@worldID", worldID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int sceneID = reader.GetInt32(0);
                        return Find(sceneID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int sceneID)
        {
            string sqlString = @"DELETE FROM Scenes 
                WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneID", sceneID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSceneRepository Delete Scene Error SceneID: {0}", sceneID);
                }
            }
        }

        public override Scene Find(int sceneID)
        {
            string sqlString = @"SELECT  
                SceneName, WorldID
                from Scenes WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
                from Scenes;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
                from Scenes WHERE WorldID = @worldID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
            string sqlString = @"UPDATE Scenes SET 
                SceneName = @sceneName, WorldID = @worldID
                WHERE SceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@sceneName", scene.SceneName);
                command.Parameters.AddWithValue("@worldID", scene.WorldID);
                command.Parameters.AddWithValue("@sceneID", scene.SceneID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSceneRepository Save Scene Error SceneID: {0}", scene.SceneID);
                }
            }
        }
    }
}
