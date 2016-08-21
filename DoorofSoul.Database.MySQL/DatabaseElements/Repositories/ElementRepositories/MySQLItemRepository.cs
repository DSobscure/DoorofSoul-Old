using System;
using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;
using DoorofSoul.Library.General.ElementComponents;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories
{
    class MySQLItemRepository : ItemRepository
    {
        public override Item Create(string itemName, string description)
        {
            string sqlString = @"INSERT INTO ItemCollection 
                (ItemName,Description) VALUES (@itemName,@description) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("itemName", itemName);
                command.Parameters.AddWithValue("description", description);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int itemID = reader.GetInt32(0);
                        return Find(itemID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int itemID)
        {
            string sqlString = @"DELETE FROM ItemCollection 
                WHERE ItemID = @itemID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemRepository Delete Item Error ItemID: {0}", itemID);
                }
            }
        }

        public override Item Find(int itemID)
        {
            string sqlString = @"SELECT  
                ItemName, Description
                from ItemCollection WHERE ItemID = @itemID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string itemName = reader.GetString(0);
                        string description = reader.GetString(1);
                        Item item = new Item(itemID, itemName, description);
                        return item;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(Item item)
        {
            string sqlString = @"UPDATE ItemCollection SET 
                ItemName = @itemName, Description = @description
                WHERE ItemID = @itemID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemName", item.ItemName);
                command.Parameters.AddWithValue("@description", item.Description);
                command.Parameters.AddWithValue("@itemID", item.ItemID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLItemRepository Save Item Error ItemID: {0}", item.ItemID);
                }
            }
        }
    }
}
