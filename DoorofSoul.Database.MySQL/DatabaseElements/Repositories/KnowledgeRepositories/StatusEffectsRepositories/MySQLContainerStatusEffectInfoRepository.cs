using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories
{
    class MySQLContainerStatusEffectInfoRepository : ContainerStatusEffectInfoRepository
    {
        public override ContainerStatusEffectInfo Create(int affectedContainerID, StatusEffect statusEffect, float effectDuration, DateTime expirationTime)
        {
            string sqlString = @"INSERT INTO ContainerStatusEffectInfoCollection 
                (AffectedContainerID,StatusEffectID,EffectDuration,ExpirationTime) VALUES (@affectedContainerID,@statusEffectID,@effectDuration,@expirationTime) ;
                SELECT LAST_INSERT_ID();";
            int containerStatusEffectInfoID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.StatusEffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@affectedContainerID", affectedContainerID);
                command.Parameters.AddWithValue("@statusEffectID", statusEffect.StatusEffectID);
                command.Parameters.AddWithValue("@effectDuration", effectDuration);
                command.Parameters.AddWithValue("@expirationTime", expirationTime);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        containerStatusEffectInfoID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(containerStatusEffectInfoID);
        }

        public override void Delete(int containerStatusEffectInfoID)
        {
            string sqlString = @"DELETE FROM ContainerStatusEffectInfoCollection 
                WHERE ContainerStatusEffectInfoID = @containerStatusEffectInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.StatusEffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerStatusEffectInfoID", containerStatusEffectInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerStatusEffectInfoRepository Delete ContainerStatusEffectInfo Error ContainerStatusEffectInfoID: {0}", containerStatusEffectInfoID);
                }
            }
        }

        public override ContainerStatusEffectInfo Find(int containerStatusEffectInfoID)
        {
            string sqlString = @"SELECT  
                AffectedContainerID, StatusEffectID, EffectDuration, ExpirationTime
                from ContainerStatusEffectInfoCollection WHERE ContainerStatusEffectInfoID = @containerStatusEffectInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.StatusEffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@containerStatusEffectInfoID", containerStatusEffectInfoID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int affectedContainerID = reader.GetInt32(0);
                        int statusEffectID = reader.GetInt32(1);
                        float effectDuration = reader.GetFloat(2);
                        DateTime expirationTime = reader.GetDateTime(3);
                        StatusEffect statusEffect = Database.Instance.KnowledgeInterface.FindStatusEffect(statusEffectID);
                        return new ContainerStatusEffectInfo(containerStatusEffectInfoID, affectedContainerID, statusEffect, effectDuration, expirationTime);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<ContainerStatusEffectInfo> ListOfAffected(int affectedContainerID)
        {
            string sqlString = @"SELECT  
                ContainerStatusEffectInfoID, StatusEffectID, EffectDuration, ExpirationTime
                from ContainerStatusEffectInfoCollection WHERE AffectedContainerID = @affectedContainerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.StatusEffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("affectedContainerID", affectedContainerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<ContainerStatusEffectInfo> infos = new List<ContainerStatusEffectInfo>();
                    while (reader.Read())
                    {
                        int containerStatusEffectInfoID = reader.GetInt32(0);
                        int statusEffectID = reader.GetInt32(1);
                        float effectDuration = reader.GetFloat(2);
                        DateTime expirationTime = reader.GetDateTime(3);
                        StatusEffect statusEffect = Database.Instance.KnowledgeInterface.FindStatusEffect(statusEffectID);
                        infos.Add(new ContainerStatusEffectInfo(containerStatusEffectInfoID, affectedContainerID, statusEffect, effectDuration, expirationTime));
                    }
                    return infos;
                }
            }
        }

        public override void Save(ContainerStatusEffectInfo containerStatusEffectInfo)
        {
            string sqlString = @"UPDATE ContainerStatusEffectInfoCollection SET 
                AffectedContainerID = @affectedContainerID, StatusEffectID = @statusEffectID, EffectDuration = @effectDuration, ExpirationTime = @expirationTime
                WHERE ContainerStatusEffectInfoID = @containerStatusEffectInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.StatusEffectsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@affectedContainerID", containerStatusEffectInfo.AffectedContainerID);
                command.Parameters.AddWithValue("@statusEffectID", containerStatusEffectInfo.StatusEffect.StatusEffectID);
                command.Parameters.AddWithValue("@effectDuration", containerStatusEffectInfo.EffectDuration);
                command.Parameters.AddWithValue("@expirationTime", containerStatusEffectInfo.ExpirationTime);
                command.Parameters.AddWithValue("@containerStatusEffectInfoID", containerStatusEffectInfo.ContainerStatusEffectInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLContainerStatusEffectInfoRepository Save ContainerStatusEffectInfo Error ContainerStatusEffectInfoID: {0}", containerStatusEffectInfo.ContainerStatusEffectInfoID);
                }
            }
        }
    }
}
