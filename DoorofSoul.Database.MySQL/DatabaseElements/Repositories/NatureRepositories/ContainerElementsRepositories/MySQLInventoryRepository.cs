using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public class MySQLInventoryRepository : InventoryRepository
    {
        public override Inventory Create(int containerID, int capacity)
        {
            string sqlString = @"INSERT INTO InventoryCollection 
            (ContainerID, Capacity) VALUES (@containerID, @capacity) ;
            SELECT LAST_INSERT_ID();";
            int inventoryID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("containerID", containerID);
                command.Parameters.AddWithValue("capacity", capacity);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inventoryID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(inventoryID);
        }

        public override void Delete(int inventoryID)
        {
            string sqlString = @"DELETE FROM InventoryCollection
            WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryRepository Delete Inventory Error InventoryID: {0}", inventoryID);
                }
            }
        }

        public override Inventory Find(int inventoryID)
        {
            string sqlString = @"SELECT  
            ContainerID, Capacity
            from InventoryCollection WHERE InventoryID = @inventoryID;";
            Inventory inventory = null;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        int capacity = reader.GetInt32(1);
                        inventory = new Inventory(inventoryID, containerID, capacity);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            Database.RepositoryList.ElementRepositoryList.ItemsRepositoryList.ItemInfoRepository.LoadItemInfos(inventory);
            return inventory;
        }

        public override Inventory FindByContainerID(int containerID)
        {
            string sqlString = @"SELECT  
            InventoryID, Capacity
            from InventoryCollection WHERE ContainerID = @containerID;";
            Inventory inventory;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int inventoryID = reader.GetInt32(0);
                        int capacity = reader.GetInt32(1);
                        inventory = new Inventory(inventoryID, containerID, capacity);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            Database.RepositoryList.ElementRepositoryList.ItemsRepositoryList.ItemInfoRepository.LoadItemInfos(inventory);
            return inventory;
        }

        public override void Save(Inventory inventory)
        {
            string sqlString = @"UPDATE InventoryCollection SET 
                ContainerID = @containerID, Capacity = @capacity
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", inventory.ContainerID);
                command.Parameters.AddWithValue("@capacity", inventory.Capacity);
                command.Parameters.AddWithValue("@inventoryID", inventory.InventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryRepository Save Inventory Error InventoryID: {0}", inventory.InventoryID);
                }
            }
            Database.RepositoryList.ElementRepositoryList.ItemsRepositoryList.ItemInfoRepository.SaveItemInfos(inventory);
        }
    }
}
