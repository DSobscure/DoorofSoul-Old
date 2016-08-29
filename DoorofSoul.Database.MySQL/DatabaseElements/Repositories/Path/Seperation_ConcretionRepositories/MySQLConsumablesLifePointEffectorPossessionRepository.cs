using DoorofSoul.Database.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories
{
    class MySQLConsumablesLifePointEffectorPossessionRepository : ConsumablesLifePointEffectorPossessionRepository
    {
        public override List<int> GetLifePointEffectorIDs(int consumablesID)
        {
            string sqlString = @"SELECT  
                LifePointEffectorID
                from ConsumablesLifePointEffectorPossessionCollection WHERE ConsumablesID = @consumablesID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<int> lifePointEffectorIDs = new List<int>();
                    while (reader.Read())
                    {
                        int lifePointEffectorID = reader.GetInt32(0);
                        lifePointEffectorIDs.Add(lifePointEffectorID);
                    }
                    return lifePointEffectorIDs;
                }
            }
        }

        public override void AddPossession(int consumablesID, int lifePointEffectorID)
        {
            string sqlString = @"INSERT INTO ConsumablesLifePointEffectorPossessionCollection 
                (ConsumablesID, LifePointEffectorID) VALUES (@consumablesID, @lifePointEffectorID) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                command.Parameters.AddWithValue("@lifePointEffectorID", lifePointEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesLifePointEffectorPossessionRepository AddPossession Error ConsumablesID: {0}, LifePointEffectorID: {1}", consumablesID, lifePointEffectorID);
                }
            }
        }

        public override void RemovePossession(int consumablesID, int lifePointEffectorID)
        {
            string sqlString = @"DELETE FROM ConsumablesLifePointEffectorPossessionCollection 
                WHERE ConsumablesID = @consumablesID AND LifePointEffectorID = @lifePointEffectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                command.Parameters.AddWithValue("@lifePointEffectorID", lifePointEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesLifePointEffectorPossessionRepository RemovePossession Error ConsumablesID: {0}, LifePointEffectorID: {1}", consumablesID, lifePointEffectorID);
                }
            }
        }
    }
}
