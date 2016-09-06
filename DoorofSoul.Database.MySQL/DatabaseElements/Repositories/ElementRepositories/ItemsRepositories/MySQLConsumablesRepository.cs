using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.ElementComponents.Items;
using DoorofSoul.Library.General.LightComponents.Effects.Effectors;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories
{
    class MySQLConsumablesRepository : ConsumablesRepository
    {
        public override Consumables Create(int itemID)
        {
            string sqlString = @"INSERT INTO ConsumablesCollection 
                (ItemID) VALUES (@itemID) ;
                SELECT LAST_INSERT_ID();";
            int consumablesID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        consumablesID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(consumablesID);
        }

        public override void Delete(int consumablesID)
        {
            string sqlString = @"DELETE FROM ConsumablesCollection 
                WHERE ConsumablesID = @consumablesID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesRepository Delete Consumables Error ConsumablesID: {0}", consumablesID);
                }
            }
        }

        public override Consumables Find(int consumablesID)
        {
            string sqlString = @"SELECT  null
                from ConsumablesCollection WHERE ConsumablesID = @consumablesID;";
            Consumables consumables = null;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        consumables =  new Consumables(consumablesID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            if(consumables != null)
            {
                List<int> effectorIDs = Database.RepositoryList.PathRepositoryList.Seperation_ConcretionRepositoryList.ConsumablesLifePointEffectorPossessionRepository.GetLifePointEffectorIDs(consumables.ConsumablesID);
                foreach(int effectID in effectorIDs)
                {
                    consumables.AddEffector(Database.RepositoryList.LightRepositoryList.EffectsRepositoryList.ContainerLifePointEffectorRepository.Find(effectID));
                }
            }
            return consumables;
        }

        public override void Save(Consumables consumables, int itemID)
        {
            string sqlString = @"UPDATE ConsumablesCollection SET 
                ItemID = @itemID
                WHERE ConsumablesID = @consumablesID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", itemID);
                command.Parameters.AddWithValue("@consumablesID", consumables.ConsumablesID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesRepository Save Consumables Error ConsumablesID: {0}", consumables.ConsumablesID);
                }
            }
            foreach(LifePointEffector effector in consumables.Effectors.OfType<LifePointEffector>())
            {
                Database.RepositoryList.LightRepositoryList.EffectsRepositoryList.ContainerLifePointEffectorRepository.Save(effector);
            }
        }

        public override void AssemblyConsumables(Item item)
        {
            string sqlString = @"SELECT  ConsumablesID
                from ConsumablesCollection WHERE ItemID = @itemID;";
            List<int> consumablesIDs = new List<int>();
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ElementConnection.ItemsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@itemID", item.ItemID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        consumablesIDs.Add(reader.GetInt32(0));
                    }
                }
            }
            foreach(int consumablesID in consumablesIDs)
            {
                item.AddComponent(Find(consumablesID));
            }
        }
    }
}
