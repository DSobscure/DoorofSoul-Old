using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ThroneComponents;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLAnswerRepository : AnswerRepository
    {
        public override Answer Find(int answerID, Player correspondingPlayer)
        {
            string sqlString = @"SELECT  
                SoulCountLimit
                from Answers WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
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
            string sqlString = @"UPDATE Answers SET 
                SoulCountLimit = @soulCountLimit
                WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@soulCountLimit", answer.SoulCountLimit);
                command.Parameters.AddWithValue("@answerID", answer.AnswerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLAnswerRepository Save Answer Error from PlayerID:{0}, IPAddress:{1}", answer.Player.PlayerID, answer.Player.LastConnectedIPAddress);
                }
            }
        }
    }
}
