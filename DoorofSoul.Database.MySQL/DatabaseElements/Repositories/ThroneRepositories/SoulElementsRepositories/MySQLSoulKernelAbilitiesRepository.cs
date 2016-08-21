using System;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;
using DoorofSoul.Library.General.ThroneComponents.SoulElements;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories
{
    class MySQLSoulKernelAbilitiesRepository : SoulKernelAbilitiesRepository
    {
        public override SoulKernelAbilities Create(int soulID, SoulKernelAbilities kernelAbilities)
        {
            string sqlString = @"INSERT INTO SoulKernelAbilitiesCollection 
            (SoulID, Creation, Destruction, Conservation, Revolution, Balance, Guidance, Mind) VALUES 
            (@soulID, @creation, @destruction, @conservation, @revolution, @balance, @guidance, @mind) ;
            SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                command.Parameters.AddWithValue("@creation", kernelAbilities.Creation);
                command.Parameters.AddWithValue("@destruction", kernelAbilities.Destruction);
                command.Parameters.AddWithValue("@conservation", kernelAbilities.Conservation);
                command.Parameters.AddWithValue("@revolution", kernelAbilities.Revolution);
                command.Parameters.AddWithValue("@balance", kernelAbilities.Balance);
                command.Parameters.AddWithValue("@guidance", kernelAbilities.Guidance);
                command.Parameters.AddWithValue("@mind", kernelAbilities.Mind);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulKernelAbilitiesRepository Create SoulAttributes Error SoulID: {0}", soulID);
                    return null;
                }
                else
                {
                    return Find(soulID);
                }
            }
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM SoulKernelAbilitiesCollection 
            WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulKernelAbilitiesRepository Delete SoulKernelAbilities Error SoulID: {0}", soulID);
                }
            }
        }

        public override SoulKernelAbilities Find(int soulID)
        {
            string sqlString = @"SELECT  
            Creation, Destruction, Conservation, Revolution, Balance, Guidance, Mind
            from SoulKernelAbilitiesCollection WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int creation = reader.GetInt32(0);
                        int destruction = reader.GetInt32(1);
                        int conservation = reader.GetInt32(2);
                        int revolution = reader.GetInt32(3);
                        int balance = reader.GetInt32(4);
                        int guidance = reader.GetInt32(5);
                        int mind = reader.GetInt32(6);
                        return new SoulKernelAbilities(
                            creation: creation,
                            destruction: destruction,
                            conservation: conservation,
                            revolution: revolution,
                            balance: balance,
                            guidance: guidance,
                            mind: mind
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(int soulID, SoulKernelAbilities kernelAbilities)
        {
            string sqlString = @"UPDATE SoulKernelAbilitiesCollection SET 
            Creation = @creation, Destruction = @destruction, Conservation = @conservation, Revolution = @revolution, Balance = @balance, Guidance = @guidance, Mind = @mind
            WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.SoulElementsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@creation", kernelAbilities.Creation);
                command.Parameters.AddWithValue("@destruction", kernelAbilities.Destruction);
                command.Parameters.AddWithValue("@conservation", kernelAbilities.Conservation);
                command.Parameters.AddWithValue("@revolution", kernelAbilities.Revolution);
                command.Parameters.AddWithValue("@balance", kernelAbilities.Balance);
                command.Parameters.AddWithValue("@guidance", kernelAbilities.Guidance);
                command.Parameters.AddWithValue("@mind", kernelAbilities.Mind);
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulKernelAbilitiesRepository Save SoulKernelAbilities Error SoulID: {0}", soulID);
                }
            }
        }
    }
}
