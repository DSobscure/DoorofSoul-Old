using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories
{
    class MySQLSkillInfoRepository : SkillInfoRepository
    {
        public override SkillInfo Create(int understanderSoulID, Skill skill, byte skillLevel, SkillPitch skillPatch)
        {
            string sqlString = @"INSERT INTO SkillInfoCollection 
                (UnderstanderSoulID,SkillID,SkillLevel,SkillPitch) VALUES (@understanderSoulID,@skillID,@skillLevel,@skillPitch) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.SkillsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@understanderSoulID", understanderSoulID);
                command.Parameters.AddWithValue("@skillID", skill.SkillID);
                command.Parameters.AddWithValue("@skillLevel", skillLevel);
                command.Parameters.AddWithValue("@skillPitch", (byte)skillPatch);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int skillInfoID = reader.GetInt32(0);
                        return Find(skillInfoID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int skillInfoID)
        {
            string sqlString = @"DELETE FROM SkillInfoCollection 
                WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.SkillsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillInfoID", skillInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSkillInfoRepository Delete SkillInfo Error SceneID: {0}", skillInfoID);
                }
            }
        }

        public override SkillInfo Find(int skillInfoID)
        {
            string sqlString = @"SELECT  
                UnderstanderSoulID, SkillID, SkillLevel, SkillPitch
                from SkillInfoCollection WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.SkillsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillInfoID", skillInfoID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int understanderSoulID = reader.GetInt32(0);
                        int skillID = reader.GetInt32(1);
                        byte level = reader.GetByte(2);
                        SkillPitch pitch = (SkillPitch)reader.GetByte(3);
                        Skill skill = Database.Instance.KnowledgeInterface.FindSkill(skillID);
                        return new SkillInfo(skillInfoID, understanderSoulID, skill, level, pitch);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<SkillInfo> ListOfUnderstander(int understanderSoulID)
        {
            string sqlString = @"SELECT  
                SkillInfoID, SkillID, SkillLevel, SkillPitch
                from SkillInfoCollection WHERE UnderstanderSoulID = @understanderSoulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.SkillsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("understanderSoulID", understanderSoulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<SkillInfo> infos = new List<SkillInfo>();
                    while (reader.Read())
                    {
                        int skillInfoID = reader.GetInt32(0);
                        int skillID = reader.GetInt32(1);
                        byte level = reader.GetByte(2);
                        SkillPitch pitch = (SkillPitch)reader.GetByte(3);
                        Skill skill = Database.Instance.KnowledgeInterface.FindSkill(skillID);
                        infos.Add(new SkillInfo(skillInfoID, understanderSoulID, skill, level, pitch));
                    }
                    return infos;
                }
            }
        }

        public override void Save(SkillInfo skillInfo)
        {
            string sqlString = @"UPDATE SkillInfoCollection SET 
                UnderstanderSoulID = @understanderSoulID, SkillID = @skillID, SkillLevel = @skillLevel, SkillPitch = @skillPitch
                WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.SkillsConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@understanderSoulID", skillInfo.UnderstanderSoulID);
                command.Parameters.AddWithValue("@skillID", skillInfo.Skill.SkillID);
                command.Parameters.AddWithValue("@skillLevel", skillInfo.SkillLevel);
                command.Parameters.AddWithValue("@skillPitch", skillInfo.SkillPitch);
                command.Parameters.AddWithValue("@skillInfoID", skillInfo.SkillInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSkillInfoRepository Save SkillInfo Error SceneID: {0}", skillInfo.SkillInfoID);
                }
            }
        }
    }
}
