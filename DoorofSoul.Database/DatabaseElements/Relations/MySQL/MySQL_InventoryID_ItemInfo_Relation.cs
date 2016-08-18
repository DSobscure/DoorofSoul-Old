using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ContainerElements;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Relations.MySQL
{
    struct DatabaseInventoryInfo
    {
        public int itemID;
        public int itemCount;
        public int positionIndex;
    }
    public class MySQL_InventoryID_ItemInfo_Relation : InventoryID_ItemInfo_Relations
    {
        public override void ClearItemInfos(int inventoryID)
        {
            string sqlString = @"DELETE FROM InventoryID_ItemInfo_Relations 
                WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@inventoryID", inventoryID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLInventoryRepository Clear ItemInfos Error InventoryID: {0}", inventoryID);
                }
            }
        }

        public override void InsertItemInfo(int inventoryID, ItemInfo itemInfo)
        {
            string sqlString = @"INSERT INTO InventoryID_ItemInfo_Relations 
                (InventoryID,ItemID,ItemCount,PositionIndex) VALUES (@inventoryID,@itemID,@itemCount,@positionIndex) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("inventoryID", inventoryID);
                command.Parameters.AddWithValue("itemID", itemInfo.item.ItemID);
                command.Parameters.AddWithValue("itemCount", itemInfo.count);
                command.Parameters.AddWithValue("positionIndex", itemInfo.positionIndex);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLInventoryRepository Insert ItemInfos Error InventoryID: {0}, ItemID: {1}, ItemCount: {1}, PositionIndex: {2}", inventoryID, itemInfo.item.ItemID, itemInfo.count, itemInfo.positionIndex);
                }
            }
        }

        public override void LoadItemInfos(Inventory inventory)
        {
            string sqlString = @"SELECT  
                ItemID, ItemCount, PositionIndex
                from InventoryID_ItemInfo_Relations WHERE InventoryID = @inventoryID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
                    foreach(DatabaseInventoryInfo info in infos)
                    {
                        Item item = DataBase.Instance.RepositoryManager.ItemRepository.Find(info.itemID);
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
