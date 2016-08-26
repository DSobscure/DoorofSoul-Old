using System;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories
{
    public class MySQLAnswerRepository : AnswerRepository
    {
        public override int Create(int soulCountLimit)
        {
            string sqlString = @"INSERT INTO AnswerCollection 
                (SoulCountLimit) VALUES (@soulCountLimit) ;
                SELECT LAST_INSERT_ID();";
            int answerID = 0;
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulCountLimit", soulCountLimit);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        answerID = reader.GetInt32(0);
                    }
                }
            }
            return answerID;
        }

        public override Answer Find(int answerID, Player correspondingPlayer)
        {
            string sqlString = @"SELECT  
                SoulCountLimit
                from AnswerCollection WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int soulCountLimit = reader.GetInt32(0);
                        Answer answer = new Answer(answerID, soulCountLimit, correspondingPlayer);
                        correspondingPlayer.ActiveAnswer(answer);
                        return answer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(Answer answer)
        {
            string sqlString = @"UPDATE AnswerCollection SET 
                SoulCountLimit = @soulCountLimit
                WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.ThroneConnection.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulCountLimit", answer.SoulCountLimit);
                command.Parameters.AddWithValue("@answerID", answer.AnswerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLAnswerRepository Save Answer Error from PlayerID:{0}, IPAddress:{1}", answer.Player.PlayerID, answer.Player.LastConnectedIPAddress);
                }
            }
        }
    }
}
