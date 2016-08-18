using DoorofSoul.Database.DatabaseElements.Repositories.Nature;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature
{
    public class MySQLEntityRepository : EntityRepository
    {
        public override Entity Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            string sqlString = @"INSERT INTO Nature_Entities 
                (EntityName, LocatedSceneID) 
                VALUES (@entityName, @locatedSceneID) ;
                SELECT LAST_INSERT_ID();";
            int entityID;
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
            DataBase.Instance.RepositoryManager.NatureList.EntityElementsList.EntitySpacePropertiesRepository.Create(entityID, spaceProperties);
            return Find(entityID);
        }

        public override void Delete(int entityID)
        {
            string sqlString = @"DELETE FROM Nature_Entities 
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", entityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntityRepository Delete Entity Error EntityID: {0}", entityID);
                }
            }
        }

        public override Entity Find(int entityID)
        {
            string sqlString = @"SELECT  
                EntityName, LocatedSceneID
                from Nature_Entities WHERE EntityID = @entityID;";
            string entityName = null;
            int locatedSceneID;
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
            EntitySpaceProperties spaceProperties = DataBase.Instance.RepositoryManager.NatureList.EntityElementsList.EntitySpacePropertiesRepository.Find(entityID);
            return new Entity(entityID, entityName, locatedSceneID, spaceProperties);
        }

        public override List<Entity> ListInScene(int sceneID)
        {
            string sqlString = @"SELECT  
                EntityID, EntityName, LocatedSceneID
                from Nature_Entities WHERE LocatedSceneID = @sceneID;";
            List<EntityInfo> entityInfos = new List<EntityInfo>();
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
                entities.Add(new Entity(info.entityID, info.entityName, info.locatedSceneID, DataBase.Instance.RepositoryManager.NatureList.EntityElementsList.EntitySpacePropertiesRepository.Find(info.entityID)));
            }
            return entities;
        }

        public override void Save(Entity entity)
        {
            string sqlString = @"UPDATE Nature_Entities SET 
                EntityName = @entityName, LocatedSceneID = @locatedSceneID
                WHERE EntityID = @entityID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityName", entity.EntityName);
                command.Parameters.AddWithValue("@locatedSceneID", entity.LocatedSceneID);
                command.Parameters.AddWithValue("@entityID", entity.EntityID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLEntityRepository Save Entity Error EntityID: {0}", entity.EntityID);
                }
            }
            DataBase.Instance.RepositoryManager.NatureList.EntityElementsList.EntitySpacePropertiesRepository.Save(entity.EntityID, entity.SpaceProperties);
        }

        struct EntityInfo
        {
            public int entityID;
            public string entityName;
            public int locatedSceneID;
        }
    }
}
