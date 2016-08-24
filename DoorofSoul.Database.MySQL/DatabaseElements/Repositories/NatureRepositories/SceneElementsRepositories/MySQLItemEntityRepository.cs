using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories
{
    class MySQLItemEntityRepository : ItemEntityRepository
    {
        public override ItemEntity Create(int itemID, int locatedSceneID, DSVector3 position)
        {
            string sqlString = @"INSERT INTO ItemEntityCollection 
                (ItemID,LocatedSceneID,PositionX,PositionY,PositionZ) VALUES (@itemID,@locatedSceneID,@positionX,@positionY,@positionZ) ;
                SELECT LAST_INSERT_ID();";
            int itemEntityID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.SceneElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemID);
                command.Parameters.AddWithValue("@locatedSceneID", locatedSceneID);
                command.Parameters.AddWithValue("@positionX", position.x);
                command.Parameters.AddWithValue("@positionY", position.y);
                command.Parameters.AddWithValue("@positionZ", position.z);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        itemEntityID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(itemEntityID);
        }

        public override void Delete(int itemEntityID)
        {
            string sqlString = @"DELETE FROM ItemEntityCollection 
                WHERE ItemEntityID = @itemEntityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.SceneElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemEntityID", itemEntityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemEntityRepository Delete ItemEntity Error ItemEntityID: {0}", itemEntityID);
                }
            }
        }

        public override ItemEntity Find(int itemEntityID)
        {
            string sqlString = @"SELECT  
                ItemID,LocatedSceneID,PositionX,PositionY,PositionZ
                from ItemEntityCollection WHERE ItemEntityID = @itemEntityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.SceneElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemEntityID", itemEntityID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int itemID = reader.GetInt32(0);
                        int locatedSceneID = reader.GetInt32(1);
                        float positionX = reader.GetFloat(2);
                        float positionY = reader.GetFloat(3);
                        float positionZ = reader.GetFloat(4);
                        return new ItemEntity(itemEntityID, itemID, locatedSceneID, new DSVector3 { x = positionX, y = positionY, z = positionZ });
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<ItemEntity> ListOfScene(int sceneID)
        {
            string sqlString = @"SELECT  
                ItemEntityID,ItemID,PositionX,PositionY,PositionZ
                from ItemEntityCollection WHERE LocatedSceneID = @sceneID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.SceneElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("sceneID", sceneID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<ItemEntity> infos = new List<ItemEntity>();
                    while (reader.Read())
                    {
                        int itemEntityID = reader.GetInt32(0);
                        int itemID = reader.GetInt32(1);
                        float positionX = reader.GetFloat(2);
                        float positionY = reader.GetFloat(3);
                        float positionZ = reader.GetFloat(4);
                        infos.Add(new ItemEntity(itemEntityID, itemID, sceneID, new DSVector3 { x = positionX, y = positionY, z = positionZ }));
                    }
                    return infos;
                }
            }
        }

        public override void Save(ItemEntity itemEntity)
        {
            string sqlString = @"UPDATE ItemEntityCollection SET 
                ItemID = @itemID, LocatedSceneID = @locatedSceneID, PositionX = @positionX, PositionY = @positionY, PositionZ = @positionZ
                WHERE ItemEntityID = @itemEntityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.SceneElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemEntity.ItemID);
                command.Parameters.AddWithValue("@locatedSceneID", itemEntity.LoacatedSceneID);
                command.Parameters.AddWithValue("@positionX", itemEntity.Position.x);
                command.Parameters.AddWithValue("@positionY", itemEntity.Position.y);
                command.Parameters.AddWithValue("@positionZ", itemEntity.Position.z);
                command.Parameters.AddWithValue("@itemEntityID", itemEntity.ItemEntityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemEntityRepository Save ItemEntity Error ItemEntityID: {0}", itemEntity.ItemEntityID);
                }
            }
        }
    }
}
