using DoorofSoul.Library.General;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLSoulRepository : SoulRepository
    {
        public override Soul Create(int answerID, string soulName)
        {
            string sqlString = @"INSERT INTO Souls 
                (AnswerID, SoulName) VALUES (@answerID, @soulName) ;
                SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answerID);
                command.Parameters.AddWithValue("@soulName", soulName);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        return Find(soulID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Delete(int soulID)
        {
            string sqlString = @"DELETE FROM Souls 
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Application.Log.ErrorFormat("MySQLSoulRepository Delete Soul Error SoulID: {0}", soulID);
                }
            }
        }

        public override Soul Find(int soulID)
        {
            string sqlString = @"SELECT  
                AnswerID, SoulName
                from Souls WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulID", soulID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int answerID = reader.GetInt32(0);
                        string soulName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        return new Soul(soulID, answerID, soulName);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override List<Soul> List()
        {
            string sqlString = @"SELECT  
                SoulID, AnswerID, SoulName
                from Souls;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Soul> souls = new List<Soul>();
                    while (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        int answerID = reader.GetInt32(1);
                        string soulName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        souls.Add(new Soul(soulID, answerID, soulName));
                    }
                    return souls;
                }
            }
        }

        public override List<Soul> ListOfAnswer(int answerID)
        {
            string sqlString = @"SELECT  
                SoulID, SoulName
                from Souls Where AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("answerID", answerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<Soul> souls = new List<Soul>();
                    while (reader.Read())
                    {
                        int soulID = reader.GetInt32(0);
                        string soulName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        souls.Add(new Soul(soulID, answerID, soulName));
                    }
                    return souls;
                }
            }
        }

        public override void Save(Soul soul)
        {
            string sqlString = @"UPDATE Souls SET 
                AnswerID = @answerID, SoulName = @soulName
                WHERE SoulID = @soulID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", soul.AnswerID);
                command.Parameters.AddWithValue("@soulName", soul.SoulName);
                command.Parameters.AddWithValue("@soulID", soul.SoulID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Application.Log.ErrorFormat("MySQLSoulRepository Save Soul Error SoulID: {0}", soul.SoulID);
                }
            }
        }
    }
}
