using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;
using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.KnowledgeRepositories
{
    class MySQLSkillRepository : SkillRepository
    {
        public override Skill Create(HeptagramSystemTypeCode systemTypeCode, SkillMediaTypeCode mediaTypeCode, string skillName, decimal basicLifePointCost, decimal basicEnergyPointCost, decimal basicCorePointCost, decimal basicSpiritPointCost)
        {
            string sqlString = @"INSERT INTO SkillCollection 
                (SystemTypeCode,MediaTypeCode,SkillName,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost) VALUES (@systemTypeCode,@mediaTypeCode,@skillName,@basicLifePointCost,@basicEnergyPointCost,@basicCorePointCost,@basicSpiritPointCost) ;
                SELECT LAST_INSERT_ID();";
            int skillID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@systemTypeCode", (byte)systemTypeCode);
                command.Parameters.AddWithValue("@mediaTypeCode", (byte)mediaTypeCode);
                command.Parameters.AddWithValue("@skillName", skillName);
                command.Parameters.AddWithValue("@basicLifePointCost", basicLifePointCost);
                command.Parameters.AddWithValue("@basicEnergyPointCost", basicEnergyPointCost);
                command.Parameters.AddWithValue("@basicCorePointCost", basicCorePointCost);
                command.Parameters.AddWithValue("@basicSpiritPointCost", basicSpiritPointCost);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        skillID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return Find(skillID);
        }

        public override void Delete(int skillID)
        {
            string sqlString = @"DELETE FROM SkillCollection 
                WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillID", skillID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSkillRepository Delete Skill Error SkillID: {0}", skillID);
                }
            }
        }

        public override Skill Find(int skillID)
        {
            string sqlString = @"SELECT  
                SystemTypeCode,MediaTypeCode,SkillName,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost
                from SkillCollection WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillID", skillID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        HeptagramSystemTypeCode systemTypeCode = (HeptagramSystemTypeCode)reader.GetByte(0);
                        SkillMediaTypeCode mediaTypeCode = (SkillMediaTypeCode)reader.GetByte(1);
                        string skillName = reader.GetString(2);
                        decimal basicLifePointCost = reader.GetDecimal(3);
                        decimal basicEnergyPointCost = reader.GetDecimal(4);
                        decimal basicCorePointCost = reader.GetDecimal(5);
                        decimal basicSpiritPointCost = reader.GetDecimal(6);
                        return new Skill(skillID, systemTypeCode, mediaTypeCode, skillName, basicLifePointCost, basicEnergyPointCost, basicCorePointCost, basicSpiritPointCost);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Skill> List()
        {
            string sqlString = @"SELECT  
                SkillID,SystemTypeCode,MediaTypeCode,SkillName,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost
                from SkillCollection;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Skill> skills = new List<Skill>();
                    while (reader.Read())
                    {
                        int skillID = reader.GetInt32(0);
                        HeptagramSystemTypeCode systemTypeCode = (HeptagramSystemTypeCode)reader.GetByte(1);
                        SkillMediaTypeCode mediaTypeCode = (SkillMediaTypeCode)reader.GetByte(2);
                        string skillName = reader.GetString(3);
                        decimal basicLifePointCost = reader.GetDecimal(4);
                        decimal basicEnergyPointCost = reader.GetDecimal(5);
                        decimal basicCorePointCost = reader.GetDecimal(6);
                        decimal basicSpiritPointCost = reader.GetDecimal(7);
                        skills.Add(new Skill(skillID, systemTypeCode, mediaTypeCode, skillName, basicLifePointCost, basicEnergyPointCost, basicCorePointCost, basicSpiritPointCost));
                    }
                    return skills;
                }
            }
        }

        public override void Save(Skill skill)
        {
            string sqlString = @"UPDATE SkillCollection SET 
                SystemTypeCode = @systemTypeCode, MediaTypeCode = @mediaTypeCode, SkillName = @skillName, BasicLifePointCost = @basicLifePointCost, BasicEnergyPointCost = @basicEnergyPointCost, BasicCorePointCost = @basicCorePointCost, BasicSpiritPointCost = @basicSpiritPointCost
                WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.KnowledgeConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillName", skill.SkillName);
                command.Parameters.AddWithValue("@systemTypeCode", (byte)skill.SystemTypeCode);
                command.Parameters.AddWithValue("@mediaTypeCode", (byte)skill.MediaTypeCode);
                command.Parameters.AddWithValue("@basicLifePointCost", skill.BasicLifePointCost);
                command.Parameters.AddWithValue("@basicEnergyPointCost", skill.BasicEnergyPointCost);
                command.Parameters.AddWithValue("@basicCorePointCost", skill.BasicCorePointCost);
                command.Parameters.AddWithValue("@basicSpiritPointCost", skill.BasicSpiritPointCost);
                command.Parameters.AddWithValue("@skillID", skill.SkillID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSkillRepository Save Skill Error WoridID: {0}", skill.SkillID);
                }
            }
        }
    }
}
