using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    struct DatabaseInventoryInfo
    {
        public int infoID;
        public int itemID;
        public int itemCount;
        public int positionIndex;
    }
    class MySQLInventoryItemInfoRepository : InventoryItemInfoRepository
    {
        public override void ClearInventoryItemInfos(int inventoryID)
        {
            string sqlString = @"DELETE FROM InventoryItemInfoCollection 
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryItemInfoCollection Clear ItemInfos Error InventoryID: {0}", inventoryID);
                }
            }
        }

        public override void DeleteInventoryItemInfo(int inventoryItemInfoID)
        {
            string sqlString = @"DELETE FROM InventoryItemInfoCollection 
                WHERE InventoryItemInfoID = @inventoryItemInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryItemInfoID", inventoryItemInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryItemInfoRepository Delete InventoryItemInfo Error InventoryItemInfoID: {0}", inventoryItemInfoID);
                }
            }
        }

        public override void InsertInventoryItemInfo(int inventoryID, InventoryItemInfo itemInfo)
        {
            string sqlString = @"INSERT INTO InventoryItemInfoCollection 
                (InventoryID,ItemID,ItemCount,PositionIndex) VALUES (@inventoryID,@itemID,@itemCount,@positionIndex) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("inventoryID", inventoryID);
                command.Parameters.AddWithValue("itemID", itemInfo.item.ItemID);
                command.Parameters.AddWithValue("itemCount", itemInfo.count);
                command.Parameters.AddWithValue("positionIndex", itemInfo.positionIndex);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryItemInfoCollection Insert ItemInfos Error InventoryID: {0}, ItemID: {1}, ItemCount: {1}, PositionIndex: {2}", inventoryID, itemInfo.item.ItemID, itemInfo.count, itemInfo.positionIndex);
                }
            }
        }

        public override void LoadInventoryItemInfos(Inventory inventory)
        {
            string sqlString = @"SELECT  
                InventoryItemInfoID, ItemID, ItemCount, PositionIndex
                from InventoryItemInfoCollection WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventory.InventoryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<DatabaseInventoryInfo> infos = new List<DatabaseInventoryInfo>();
                    while (reader.Read())
                    {
                        int infoID = reader.GetInt32(0);
                        int itemID = reader.GetInt32(1);
                        int itemCount = reader.GetInt32(2);
                        int positionIndex = reader.GetInt32(3);
                        infos.Add(new DatabaseInventoryInfo { infoID = infoID, itemID = itemID, itemCount = itemCount, positionIndex = positionIndex });
                    }
                    foreach (DatabaseInventoryInfo info in infos)
                    {
                        Item item = Database.RepositoryList.ElementRepositoryList.ItemRepository.Find(info.itemID);
                        inventory.LoadItem(info.infoID, item, info.itemCount, info.positionIndex);
                    }
                }
            }
        }

        public override void SaveInventoryItemInfo(InventoryItemInfo inventoryItemInfo, int inventoryID)
        {
            string sqlString = @"UPDATE InventoryItemInfoCollection SET 
                InventoryID = @inventoryID, ItemID = @itemID, ItemCount = @itemCount, PositionIndex = @positionIndex
                WHERE InventoryItemInfoID = @inventoryItemInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.NatureConnection.ContainerElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                command.Parameters.AddWithValue("@itemID", inventoryItemInfo.item.ItemID);
                command.Parameters.AddWithValue("@itemCount", inventoryItemInfo.count);
                command.Parameters.AddWithValue("@positionIndex", inventoryItemInfo.positionIndex);
                command.Parameters.AddWithValue("@inventoryItemInfoID", inventoryItemInfo.inventoryItemInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLInventoryItemInfoRepository Save InventoryItemInfo Error InventoryItemInfoID: {0}", inventoryItemInfo.inventoryItemInfoID);
                }
            }
        }

        public override void SaveInventoryItemInfos(Inventory inventory)
        {
            foreach (InventoryItemInfo info in inventory.ItemInfos)
            {
                SaveInventoryItemInfo(info, inventory.InventoryID);
            }
        }
    }
}
