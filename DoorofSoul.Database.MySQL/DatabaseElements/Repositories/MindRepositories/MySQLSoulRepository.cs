using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Library.General.MindComponents.SoulElements;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.MindRepositories
{
    public class MySQLSoulRepository : SoulRepository
    {
        public override Soul Create(Answer answer, string soulName, SoulKernelTypeCode mainSoulType)
        {
            string sqlString = @"INSERT INTO SoulCollection 
                (AnswerID, SoulName) VALUES (@answerID, @soulName) ;
                SELECT LAST_INSERT_ID();";
            int soulID;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answer.AnswerID);
                command.Parameters.AddWithValue("@soulName", soulName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        soulID = reader.GetInt32(0);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulAttributesRepository.Create(soulID, SoulAttributes.GetDefaultAttribute(mainSoulType));
            return Find(soulID, answer);
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM SoulCollection 
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulRepository Delete Soul Error SoulID: {0}", soulID);
                }
            }
        }

        public override Soul Find(int soulID, Answer answer)
        {
            string sqlString = @"SELECT  
                AnswerID, SoulName
                from SoulCollection WHERE SoulID = @soulID;";
            SoulAttributes attributes = Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulAttributesRepository.Find(soulID);
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int answerID = reader.GetInt32(0);
                        string soulName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        if(answerID == answer.AnswerID)
                        {
                            return new Soul(soulID, answer, soulName, attributes);
                        }
                        else
                        {
                            Database.Log.ErrorFormat("MySQLSoulRepository Find Soul Error Answer Incorrect SoulID: {0}, AnswerID: {1},{2}", soulID, answerID, answer.AnswerID);
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Soul> ListOfAnswer(Answer answer)
        {
            string sqlString = @"SELECT  
                SoulID
                from SoulCollection Where AnswerID = @answerID;";
            List<int> soulIDs = new List<int>();
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("answerID", answer.AnswerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        soulIDs.Add(soulID);
                    }
                }
            }
            List<Soul> souls = new List<Soul>();
            foreach(int soulID in soulIDs)
            {
                Soul soul = Find(soulID, answer);
                if(soul != null)
                {
                    souls.Add(soul);
                }
            }
            return souls;
        }

        public override void Save(Soul soul)
        {
            string sqlString = @"UPDATE SoulCollection SET 
                AnswerID = @answerID, SoulName = @soulName
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.MindConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", soul.AnswerID);
                command.Parameters.AddWithValue("@soulName", soul.SoulName);
                command.Parameters.AddWithValue("@soulID", soul.SoulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLSoulRepository Save Soul Error SoulID: {0}", soul.SoulID);
                }
            }
            Database.RepositoryList.MindRepositoryList.SoulElementsRepositoryList.SoulAttributesRepository.Save(soul.SoulID, soul.Attributes);
        }
    }
}
