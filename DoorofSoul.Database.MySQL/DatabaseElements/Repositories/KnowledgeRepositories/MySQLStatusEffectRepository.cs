using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLStatusEffectRepository : StatusEffectRepository
    {
        public override StatusEffect Create(string statusEffectName, float standardEffectDuration)
        {
            string sqlString = @"INSERT INTO StatusEffectCollection 
                (StatusEffectName,StandardEffectDuration) VALUES (@statusEffectName,@standardEffectDuration) ;
                SELECT LAST_INSERT_ID();";
            int statusEffectID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@statusEffectName", statusEffectName);
                command.Parameters.AddWithValue("@standardEffectDuration", standardEffectDuration);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        statusEffectID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(statusEffectID);
        }

        public override void Delete(int statusEffectID)
        {
            string sqlString = @"DELETE FROM StatusEffectCollection 
                WHERE StatusEffectID = @statusEffectID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@statusEffectID", statusEffectID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLStatusEffectRepository Delete StatusEffect Error StatusEffectID: {0}", statusEffectID);
                }
            }
        }

        public override StatusEffect Find(int statusEffectID)
        {
            string sqlString = @"SELECT  
                StatusEffectName,StandardEffectDuration
                from StatusEffectCollection WHERE StatusEffectID = @statusEffectID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@statusEffectID", statusEffectID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string statusEffectName = reader.GetString(0);
                        float standardEffectDuration = reader.GetFloat(1);
                        return new StatusEffect(statusEffectID, statusEffectName, standardEffectDuration);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<StatusEffect> List()
        {
            string sqlString = @"SELECT  
                StatusEffectID,StatusEffectName,StandardEffectDuration
                from StatusEffectCollection;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<StatusEffect> statusEffects = new List<StatusEffect>();
                    while (reader.Read())
                    {
                        int statusEffectID = reader.GetInt32(0);
                        string statusEffectName = reader.GetString(1);
                        float standardEffectDuration = reader.GetFloat(2);
                        statusEffects.Add(new StatusEffect(statusEffectID, statusEffectName, standardEffectDuration));
                    }
                    return statusEffects;
                }
            }
        }

        public override void Save(StatusEffect statusEffect)
        {
            string sqlString = @"UPDATE StatusEffectCollection SET 
                StatusEffectName = @statusEffectName, StandardEffectDuration = @standardEffectDuration
                WHERE StatusEffectID = @statusEffectID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@statusEffectName", statusEffect.StatusEffectName);
                command.Parameters.AddWithValue("@standardEffectDuration", statusEffect.StandardEffectDuration);
                command.Parameters.AddWithValue("@statusEffectID", statusEffect.StatusEffectID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLStatusEffectRepository Save StatusEffect Error StatusEffectID: {0}", statusEffect.StatusEffectID);
                }
            }
        }
    }
}
