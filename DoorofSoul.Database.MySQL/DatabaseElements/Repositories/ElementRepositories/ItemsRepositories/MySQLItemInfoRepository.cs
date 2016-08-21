using System;
using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;
using DoorofSoul.Library.General.ElementComponents.Items;
using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.ElementComponents;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories
{
    struct DatabaseInventoryInfo
    {
        public int itemID;
        public int itemCount;
        public int positionIndex;
    }
    class MySQLItemInfoRepository : ItemInfoRepository
    {
        public override void ClearItemInfos(int inventoryID)
        {
            string sqlString = @"DELETE FROM ItemInfoCollection 
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemInfoRepository Clear ItemInfos Error InventoryID: {0}", inventoryID);
                }
            }
        }

        public override void InsertItemInfo(int inventoryID, ItemInfo itemInfo)
        {
            string sqlString = @"INSERT INTO ItemInfoCollection 
                (InventoryID,ItemID,ItemCount,PositionIndex) VALUES (@inventoryID,@itemID,@itemCount,@positionIndex) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("inventoryID", inventoryID);
                command.Parameters.AddWithValue("itemID", itemInfo.item.ItemID);
                command.Parameters.AddWithValue("itemCount", itemInfo.count);
                command.Parameters.AddWithValue("positionIndex", itemInfo.positionIndex);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemInfoRepository Insert ItemInfos Error InventoryID: {0}, ItemID: {1}, ItemCount: {1}, PositionIndex: {2}", inventoryID, itemInfo.item.ItemID, itemInfo.count, itemInfo.positionIndex);
                }
            }
        }

        public override void LoadItemInfos(Inventory inventory)
        {
            string sqlString = @"SELECT  
                ItemID, ItemCount, PositionIndex
                from ItemInfoCollection WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventory.InventoryID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<DatabaseInventoryInfo> infos = new List<DatabaseInventoryInfo>();
                    while (reader.Read())
                    {
                        int itemID = reader.GetInt32(0);
                        int itemCount = reader.GetInt32(1);
                        int positionIndex = reader.GetInt32(2);
                        infos.Add(new DatabaseInventoryInfo { itemID = itemID, itemCount = itemCount, positionIndex = positionIndex });
                    }
                    foreach (DatabaseInventoryInfo info in infos)
                    {
                        Item item = Database.RepositoryList.ElementRepositoryList.ItemRepository.Find(info.itemID);
                        inventory.LoadItem(item, info.itemCount, info.positionIndex);
                    }
                }
            }
        }

        public override void SaveItemInfos(Inventory inventory)
        {
            ClearItemInfos(inventory.InventoryID);
            foreach (ItemInfo info in inventory.ItemInfos)
            {
                InsertItemInfo(inventory.InventoryID, info);
            }
        }
    }
}
