using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;
using DoorofSoul.Library.General.LightComponents.Effects.Effectors;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories.EffectsRepositories
{
    class MySQLShooterAbilitiesEffectorRepository : ShooterAbilitiesEffectorRepository
    {
        public override ShooterAbilitiesEffector Create(ShooterAbilitiesTypeCode AbilityType, int effectValue)
        {
            string sqlString = @"INSERT INTO ShooterAbilitiesEffectorCollection 
                (AbilityType,EffectValue) VALUES (@abilityType,@effectValue) ;
                SELECT LAST_INSERT_ID();";
            int effectorID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@abilityType", (byte)AbilityType);
                command.Parameters.AddWithValue("@effectValue", effectValue);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        effectorID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(effectorID);
        }

        public override void Delete(int effectorID)
        {
            string sqlString = @"DELETE FROM ShooterAbilitiesEffectorCollection 
                WHERE ShooterAbilitiesEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@effectorID", effectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLShooterAbilitiesEffectorRepository Delete ShooterAbilitiesEffector Error ShooterAbilitiesEffectorID: {0}", effectorID);
                }
            }
        }

        public override ShooterAbilitiesEffector Find(int effectorID)
        {
            string sqlString = @"SELECT  
                AbilityType, EffectValue
                from ShooterAbilitiesEffectorCollection WHERE ShooterAbilitiesEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@effectorID", effectorID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ShooterAbilitiesTypeCode abilityType = (ShooterAbilitiesTypeCode)reader.GetByte(0);
                        int effectValue = reader.GetInt32(1);
                        return new ShooterAbilitiesEffector(effectorID, abilityType, effectValue);
                    }
                    else
                    {
                        return null;
                    }
                }
            };
        }

        public override void Save(ShooterAbilitiesEffector effector)
        {
            string sqlString = @"UPDATE ShooterAbilitiesEffectorCollection SET 
                AbilityType = @abilityType ,EffectValue = @effectValue
                WHERE LifePointEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@abilityType", (byte)effector.AbilityType);
                command.Parameters.AddWithValue("@effectValue", effector.EffectValue);
                command.Parameters.AddWithValue("@effectorID", effector.ShooterAbilitiesEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLShooterAbilitiesEffectorRepository Save ShooterAbilitiesEffector Error ShooterAbilitiesEffectorID: {0}", effector.ShooterAbilitiesEffectorID);
                }
            }
        }
    }
}
