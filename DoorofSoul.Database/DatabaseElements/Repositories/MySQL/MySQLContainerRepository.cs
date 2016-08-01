using DoorofSoul.Library.General;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLContainerRepository : ContainerRepository
    {
        public override Container Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            Entity entity = DataBase.Instance.RepositoryManager.EntityRepository.Create(entityName, locatedSceneID, spaceProperties);
            if (entity != null)
            {
                string sqlString = @"INSERT INTO Containers 
                (EntityID) VALUES (@entityID) ;
                SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("entityID", entity.EntityID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int containerID = reader.GetInt32(0);
                            return Find(containerID);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM Containers 
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLContainerRepository Delete Container Error ContainerID: {0}", containerID);
                }
            }
        }

        public override Container Find(int containerID)
        {
            string sqlString = @"SELECT  
                EntityID, ContainerName
                from Containers WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int entityID = reader.GetInt32(0);
                        string containerName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        Entity entity = DataBase.Instance.RepositoryManager.EntityRepository.Find(entityID);
                        Container container = new Container(containerID, entityID, containerName);
                        container.BindEntity(entity);
                        return container;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Container> List()
        {
            string sqlString = @"SELECT  
                ContainerID, EntityID, ContainerName
                from Containers;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Container> containers = new List<Container>();
                    while (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        int entityID = reader.GetInt32(1);
                        string containerName = reader.IsDBNull(2) ? null : reader.GetString(2);
                        Entity entity = DataBase.Instance.RepositoryManager.EntityRepository.Find(entityID);
                        Container container = new Container(containerID, entityID, containerName);
                        container.BindEntity(entity);
                        containers.Add(container);
                    }
                    return containers;
                }
            }
        }

        public override void Save(Container container)
        {
            string sqlString = @"UPDATE Containers SET 
                EntityID = @entityID
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", container.EntityID);
                command.Parameters.AddWithValue("@containerID", container.ContainerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLContainerRepository Save Container Error ContainerID: {0}", container.ContainerID);
                    DataBase.Instance.RepositoryManager.EntityRepository.Save(container.Entity);
                }
            }
        }
    }
}
