using DoorofSoul.Database.DatabaseElements.Repositories.LightRepositories.EffectsRepositories;
using DoorofSoul.Library.General.LightComponents.Effects.Effectors;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LightRepositories.EffectsRepositories
{
    class MySQLLifePointEffectorRepository : LifePointEffectorRepository
    {
        public override LifePointEffector Create(decimal effectValue)
        {
            string sqlString = @"INSERT INTO LifePointEffectorCollection 
                (EffectValue) VALUES (@effectValue) ;
                SELECT LAST_INSERT_ID();";
            int effectorID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
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
            string sqlString = @"DELETE FROM LifePointEffectorCollection 
                WHERE LifePointEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@effectorID", effectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLLifePointEffectorRepository Delete LifePointEffector Error LifePointEffector: {0}", effectorID);
                }
            }
        }

        public override LifePointEffector Find(int effectorID)
        {
            string sqlString = @"SELECT  
                EffectValue
                from LifePointEffectorCollection WHERE LifePointEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@effectorID", effectorID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal effectValue = reader.GetDecimal(0);
                        return new LifePointEffector(effectorID, effectValue);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(LifePointEffector effector)
        {
            string sqlString = @"UPDATE LifePointEffectorCollection SET 
                EffectValue = @effectValue
                WHERE LifePointEffectorID = @effectorID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.LightConnection.EffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@effectValue", effector.EffectValue);
                command.Parameters.AddWithValue("@effectorID", effector.LifePointEffectorID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLLifePointEffectorRepository Save LifePointEffector Error LifePointEffector: {0}", effector.LifePointEffectorID);
                }
            }
        }
    }
}
