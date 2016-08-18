using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLElementSkillRepository : ElementSkillRepository
    {
        public override Skill Create(string skillName, SkillMediaTypeCode mediaTypeCode, decimal basicLifePointCost, decimal basicEnergyPointCost, decimal basicCorePointCost, decimal basicSpiritPointCost)
        {
            string sqlString = @"INSERT INTO ElementSkills 
                (SkillName,MediaTypeCode,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost) VALUES (@skillName,@mediaTypeCode,@basicLifePointCost,@basicEnergyPointCost,@basicCorePointCost,@basicSpiritPointCost) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillName", skillName);
                command.Parameters.AddWithValue("@mediaTypeCode", (byte)mediaTypeCode);
                command.Parameters.AddWithValue("@basicLifePointCost", basicLifePointCost);
                command.Parameters.AddWithValue("@basicEnergyPointCost", basicEnergyPointCost);
                command.Parameters.AddWithValue("@basicCorePointCost", basicCorePointCost);
                command.Parameters.AddWithValue("@basicSpiritPointCost", basicSpiritPointCost);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int skillID = reader.GetInt32(0);
                        return Find(skillID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int skillID)
        {
            string sqlString = @"DELETE FROM ElementSkills 
                WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillID", skillID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLElementSkillRepository Delete Skill Error SkillID: {0}", skillID);
                }
            }
        }

        public override Skill Find(int skillID)
        {
            string sqlString = @"SELECT  
                MediaTypeCode,SkillName,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost
                from ElementSkills WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillID", skillID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SkillMediaTypeCode mediaTypeCode = (SkillMediaTypeCode)reader.GetByte(0);
                        string skillName = reader.GetString(1);
                        decimal basicLifePointCost = reader.GetDecimal(2);
                        decimal basicEnergyPointCost = reader.GetDecimal(3);
                        decimal basicCorePointCost = reader.GetDecimal(4);
                        decimal basicSpiritPointCost = reader.GetDecimal(5);
                        return new Skill(skillID, HeptagramSystemTypeCode.Element, mediaTypeCode, skillName, basicLifePointCost, basicEnergyPointCost, basicCorePointCost, basicSpiritPointCost);
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
                SkillID,MediaTypeCode,SkillName,BasicLifePointCost,BasicEnergyPointCost,BasicCorePointCost,BasicSpiritPointCost
                from ElementSkills;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Skill> skills = new List<Skill>();
                    while (reader.Read())
                    {
                        int skillID = reader.GetInt32(0);
                        SkillMediaTypeCode mediaTypeCode = (SkillMediaTypeCode)reader.GetByte(1);
                        string skillName = reader.GetString(2);
                        decimal basicLifePointCost = reader.GetDecimal(3);
                        decimal basicEnergyPointCost = reader.GetDecimal(4);
                        decimal basicCorePointCost = reader.GetDecimal(5);
                        decimal basicSpiritPointCost = reader.GetDecimal(6);
                        skills.Add(new Skill(skillID, HeptagramSystemTypeCode.Element, mediaTypeCode, skillName, basicLifePointCost, basicEnergyPointCost, basicCorePointCost, basicSpiritPointCost));
                    }
                    return skills;
                }
            }
        }

        public override void Save(Skill skill)
        {
            string sqlString = @"UPDATE ElementSkills SET 
                SkillName = @skillName, MediaTypeCode = @mediaTypeCode, BasicLifePointCost = @basicLifePointCost, BasicEnergyPointCost = @basicEnergyPointCost, BasicCorePointCost = @basicCorePointCost, BasicSpiritPointCost = @basicSpiritPointCost
                WHERE SkillID = @skillID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@skillName", skill.SkillName);
                command.Parameters.AddWithValue("@mediaTypeCode", (byte)skill.MediaTypeCode);
                command.Parameters.AddWithValue("@basicLifePointCost", skill.BasicLifePointCost);
                command.Parameters.AddWithValue("@basicEnergyPointCost", skill.BasicEnergyPointCost);
                command.Parameters.AddWithValue("@basicCorePointCost", skill.BasicCorePointCost);
                command.Parameters.AddWithValue("@basicSpiritPointCost", skill.BasicSpiritPointCost);
                command.Parameters.AddWithValue("@skillID", skill.SkillID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLElementSkillRepository Save Skill Error WoridID: {0}", skill.SkillID);
                }
            }
        }
    }
}
