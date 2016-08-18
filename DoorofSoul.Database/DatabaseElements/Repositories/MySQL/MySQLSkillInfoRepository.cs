using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using MySql.Data.MySqlClient;
using DoorofSoul.Library.General.KnowledgeElements.Skills;
using DoorofSoul.Library.General.SoulElements;
using DoorofSoul.Protocol;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    class MySQLSkillInfoRepository : SkillInfoRepository
    {
        public override SkillInfo Create(int understanderSoulID, Skill skill, byte skillLevel, SkillPitch skillPatch)
        {
            string sqlString = @"INSERT INTO SkillInfos 
                (UnderstanderSoulID,SkillSystemTypeCode,SkillID,SkillLevel,SkillPitch) VALUES (@understanderSoulID, @skillSystemTypeCode,@skillID,@skillLevel,@skillPitch) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@understanderSoulID", understanderSoulID);
                command.Parameters.AddWithValue("@skillSystemTypeCode", (byte)skill.SystemTypeCode);
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
            string sqlString = @"DELETE FROM SkillInfos 
                WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillInfoID", skillInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSkillInfoRepository Delete SkillInfo Error SceneID: {0}", skillInfoID);
                }
            }
        }

        public override SkillInfo Find(int skillInfoID)
        {
            string sqlString = @"SELECT  
                UnderstanderSoulID, SkillSystemTypeCode, SkillID, SkillLevel, SkillPitch
                from SkillInfos WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillInfoID", skillInfoID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int understanderSoulID = reader.GetInt32(0);
                        HeptagramSystemTypeCode systemTypeCode = (HeptagramSystemTypeCode)reader.GetByte(1);
                        int skillID = reader.GetInt32(2);
                        byte level = reader.GetByte(3);
                        SkillPitch pitch = (SkillPitch)reader.GetByte(4);
                        Skill skill = DataBase.Instance.KnowledgeInterface.FindSkill(systemTypeCode, skillID);
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
                SkillInfoID, SkillSystemTypeCode, SkillID, SkillLevel, SkillPitch
                from SkillInfos WHERE UnderstanderSoulID = @understanderSoulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("understanderSoulID", understanderSoulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<SkillInfo> infos = new List<SkillInfo>();
                    while (reader.Read())
                    {
                        int skillInfoID = reader.GetInt32(0);
                        HeptagramSystemTypeCode systemTypeCode = (HeptagramSystemTypeCode)reader.GetByte(1);
                        int skillID = reader.GetInt32(2);
                        byte level = reader.GetByte(3);
                        SkillPitch pitch = (SkillPitch)reader.GetByte(4);
                        Skill skill = DataBase.Instance.KnowledgeInterface.FindSkill(systemTypeCode, skillID);
                        infos.Add(new SkillInfo(skillInfoID, understanderSoulID, skill, level, pitch));
                    }
                    return infos;
                }
            }
        }

        public override void Save(SkillInfo skillInfo)
        {
            string sqlString = @"UPDATE SkillInfos SET 
                UnderstanderSoulID = @understanderSoulID, SkillSystemTypeCode = @skillSystemTypeCode, SkillID = @skillID, SkillLevel = @skillLevel, SkillPitch = @skillPitch
                WHERE SkillInfoID = @skillInfoID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@understanderSoulID", skillInfo.UnderstanderSoulID);
                command.Parameters.AddWithValue("@skillSystemTypeCode", skillInfo.Skill.SystemTypeCode);
                command.Parameters.AddWithValue("@skillID", skillInfo.Skill.SkillID);
                command.Parameters.AddWithValue("@skillLevel", skillInfo.SkillLevel);
                command.Parameters.AddWithValue("@skillPitch", skillInfo.SkillPitch);
                command.Parameters.AddWithValue("@skillInfoID", skillInfo.SkillInfoID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLSkillInfoRepository Save SkillInfo Error SceneID: {0}", skillInfo.SkillInfoID);
                }
            }
        }
    }
}
