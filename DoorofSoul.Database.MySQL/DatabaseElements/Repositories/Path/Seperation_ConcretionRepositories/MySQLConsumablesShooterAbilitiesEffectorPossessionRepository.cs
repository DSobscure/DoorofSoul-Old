using DoorofSoul.Database.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path.Seperation_ConcretionRepositories
{
    class MySQLConsumablesShooterAbilitiesEffectorPossessionRepository : ConsumablesShooterAbilitiesEffectorPossessionRepository
    {
        public override List<int> GetShooterAbilitiesEffectorIDs(int consumablesID)
        {
            string sqlString = @"SELECT  
                ShooterAbilitiesEffectorID
                from ConsumablesShooterAbilitiesEffectorPossessionCollection WHERE ConsumablesID = @consumablesID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<int> shooterAbilitiesEffectorIDs = new List<int>();
                    while (reader.Read())
                    {
                        int lifePointEffectorID = reader.GetInt32(0);
                        shooterAbilitiesEffectorIDs.Add(lifePointEffectorID);
                    }
                    return shooterAbilitiesEffectorIDs;
                }
            }
        }
        public override void AddPossession(int consumablesID, int shooterAbilitiesEffectorID)
        {
            string sqlString = @"INSERT INTO ConsumablesShooterAbilitiesEffectorPossessionCollection 
                (ConsumablesID, ShooterAbilitiesEffectorID) VALUES (@consumablesID, @shooterAbilitiesEffectorID) ;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                command.Parameters.AddWithValue("@shooterAbilitiesEffectorID", shooterAbilitiesEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesShooterAbilitiesEffectorPossessionRepository AddPossession Error ConsumablesID: {0}, ShooterAbilitiesEffectorID: {1}", consumablesID, shooterAbilitiesEffectorID);
                }
            }
        }

        public override void RemovePossession(int consumablesID, int shooterAbilitiesEffectorID)
        {
            string sqlString = @"DELETE FROM ConsumablesShooterAbilitiesEffectorPossessionCollection 
                WHERE ConsumablesID = @consumablesID AND ShooterAbilitiesEffectorID = @shooterAbilitiesEffectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.PathConnectionList.Seperation_ConcretionConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@consumablesID", consumablesID);
                command.Parameters.AddWithValue("@shooterAbilitiesEffectorID", shooterAbilitiesEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLConsumablesShooterAbilitiesEffectorPossessionRepository RemovePossession Error ConsumablesID: {0}, ShooterAbilitiesEffectorID: {1}", consumablesID, shooterAbilitiesEffectorID);
                }
            }
        }
    }
}
