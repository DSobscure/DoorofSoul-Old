using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    public class MySQLContainerRepository : ContainerRepository
    {
        public override Container Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            Entity entity = Database.RepositoryList.NatureRepositoryList.EntityRepository.Create(entityName, locatedSceneID, spaceProperties);
            if (entity != null)
            {
                string sqlString = @"INSERT INTO ContainerCollection 
                (EntityID) VALUES (@entityID) ;
                SELECT LAST_INSERT_ID();";
                int containerID;
                using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("entityID", entity.EntityID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            containerID = reader.GetInt32(0);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerAttributesRepository.Create(containerID, ContainerAttributes.GetDefaultAttribute());
                Inventory inventory = Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.InventoryRepository.Create(containerID, Inventory.GetDefaultCapacity());
                return Find(containerID);
            }
            else
            {
                return null;
            }
        }

        public override void Delete(int containerID)
        {
            string sqlString = @"DELETE FROM ContainerCollection 
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerRepository Delete Container Error ContainerID: {0}", containerID);
                }
            }
        }

        public override Container Find(int containerID)
        {
            string sqlString = @"SELECT  
            EntityID, ContainerName
            from ContainerCollection WHERE ContainerID = @containerID;";
            int entityID;
            string containerName;
            Inventory inventory = Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.InventoryRepository.FindByContainerID(containerID);
            ContainerAttributes attributes = Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerAttributesRepository.Find(containerID);
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        entityID = reader.GetInt32(0);
                        containerName = reader.IsDBNull(1) ? null : reader.GetString(1);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            Entity entity = Database.RepositoryList.NatureRepositoryList.EntityRepository.Find(entityID);
            Container container = new Container(containerID, entityID, containerName, attributes);
            container.BindEntity(entity);
            container.BindInventory(inventory);
            return container;
        }

        public override List<Container> List()
        {
            string sqlString = @"SELECT  
                ContainerID
                from ContainerCollection;";
            List<int> containerIDs = new List<int>();
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        containerIDs.Add(containerID);
                    }
                }
            }

            List<Container> containers = new List<Container>();
            foreach (int containerID in containerIDs)
            {
                Container container = Find(containerID);
                if(container != null)
                {
                    containers.Add(container);
                }
            }
            return containers;
        }

        public override void Save(Container container)
        {
            string sqlString = @"UPDATE ContainerCollection SET 
                EntityID = @entityID, ContainerName = @containerName
                WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@entityID", container.EntityID);
                command.Parameters.AddWithValue("@containerName", container.ContainerName);
                command.Parameters.AddWithValue("@containerID", container.ContainerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerRepository Save Container Error ContainerID: {0}", container.ContainerID);
                }
            }
            Database.RepositoryList.NatureRepositoryList.ContainerElementsRepositoryList.ContainerAttributesRepository.Save(container.ContainerID, container.Attributes);
        }
    }
}
