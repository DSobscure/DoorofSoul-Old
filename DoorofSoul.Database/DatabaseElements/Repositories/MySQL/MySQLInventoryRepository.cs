using DoorofSoul.Library.General.ContainerElements;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLInventoryRepository : InventoryRepository
    {
        public override Inventory Create(int containerID, int capacity)
        {
            string sqlString = @"INSERT INTO Inventories 
                (ContainerID, Capacity) VALUES (@containerID, @capacity) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("containerID", containerID);
                command.Parameters.AddWithValue("capacity", capacity);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int inventoryID = reader.GetInt32(0);
                        return Find(inventoryID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int inventoryID)
        {
            string sqlString = @"DELETE FROM Inventories 
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLInventoryRepository Delete Inventory Error InventoryID: {0}", inventoryID);
                }
            }
        }

        public override Inventory Find(int inventoryID)
        {
            string sqlString = @"SELECT  
                ContainerID, Capacity
                from Inventories WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int containerID = reader.GetInt32(0);
                        int capacity = reader.GetInt32(1);
                        Inventory inventory = new Inventory(inventoryID, containerID, capacity);
                        DataBase.Instance.RelationManager.InventoryItemInfo_Relation.LoadItemInfos(inventory);
                        return inventory;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override Inventory FindByContainerID(int containerID)
        {
            string sqlString = @"SELECT  
                InventoryID, Capacity
                from Inventories WHERE ContainerID = @containerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", containerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int inventoryID = reader.GetInt32(0);
                        int capacity = reader.GetInt32(1);
                        Inventory inventory = new Inventory(inventoryID, containerID, capacity);
                        DataBase.Instance.RelationManager.InventoryItemInfo_Relation.LoadItemInfos(inventory);
                        return inventory;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(Inventory inventory)
        {
            string sqlString = @"UPDATE Inventories SET 
                ContainerID = @containerID, Capacity = @capacity
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerID", inventory.ContainerID);
                command.Parameters.AddWithValue("@capacity", inventory.Capacity);
                command.Parameters.AddWithValue("@inventoryID", inventory.InventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLInventoryRepository Save Inventory Error InventoryID: {0}", inventory.InventoryID);
                }
                else
                {
                    DataBase.Instance.RelationManager.InventoryItemInfo_Relation.SaveItemInfos(inventory);
                }
            }
        }
    }
}
