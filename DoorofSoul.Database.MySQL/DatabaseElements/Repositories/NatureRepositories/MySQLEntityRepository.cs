using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    public class MySQLEntityRepository : EntityRepository
    {
        public override Entity Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            string sqlString = @"INSERT INTO EntityCollection 
                (EntityName, LocatedSceneID) 
                VALUES (@entityName, @locatedSceneID) ;
                SELECT LAST_INSERT_ID();";
            int entityID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityName", entityName);
                command.Parameters.AddWithValue("@locatedSceneID", locatedSceneID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        entityID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            Database.RepositoryList.NatureRepositoryList.EntityElementsRepositoryList.EntitySpacePropertiesRepository.Create(entityID, spaceProperties);
            return Find(entityID);
        }

        public override void Delete(int entityID)
        {
            string sqlString = @"DELETE FROM EntityCollection 
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLEntityRepository Delete Entity Error EntityID: {0}", entityID);
                }
            }
        }

        public override Entity Find(int entityID)
        {
            string sqlString = @"SELECT  
                EntityName, LocatedSceneID
                from EntityCollection WHERE EntityID = @entityID;";
            string entityName = null;
            int locatedSceneID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        entityName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        locatedSceneID = reader.IsDBNull(1) ? -1 : reader.GetInt32(1);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            EntitySpaceProperties spaceProperties = Database.RepositoryList.NatureRepositoryList.EntityElementsRepositoryList.EntitySpacePropertiesRepository.Find(entityID);
            return new Entity(entityID, entityName, locatedSceneID, spaceProperties);
        }

        public override List<Entity> ListInScene(int sceneID)
        {
            string sqlString = @"SELECT  
                EntityID, EntityName, LocatedSceneID
                from EntityCollection WHERE LocatedSceneID = @sceneID;";
            List<EntityInfo> entityInfos = new List<EntityInfo>();
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("sceneID", sceneID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        string entityName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        int locatedSceneID = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                        entityInfos.Add(new EntityInfo { entityID = entityID, entityName = entityName, locatedSceneID = locatedSceneID });
                    }
                }
            }
            List<Entity> entities = new List<Entity>();
            foreach(EntityInfo info in entityInfos)
            {
                entities.Add(new Entity(info.entityID, info.entityName, info.locatedSceneID, Database.RepositoryList.NatureRepositoryList.EntityElementsRepositoryList.EntitySpacePropertiesRepository.Find(info.entityID)));
            }
            return entities;
        }

        public override void Save(Entity entity)
        {
            string sqlString = @"UPDATE EntityCollection SET 
                EntityName = @entityName, LocatedSceneID = @locatedSceneID
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityName", entity.EntityName);
                command.Parameters.AddWithValue("@locatedSceneID", entity.LocatedSceneID);
                command.Parameters.AddWithValue("@entityID", entity.EntityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLEntityRepository Save Entity Error EntityID: {0}", entity.EntityID);
                }
            }
            Database.RepositoryList.NatureRepositoryList.EntityElementsRepositoryList.EntitySpacePropertiesRepository.Save(entity.EntityID, entity.SpaceProperties);
        }

        struct EntityInfo
        {
            public int entityID;
            public string entityName;
            public int locatedSceneID;
        }
    }
}
