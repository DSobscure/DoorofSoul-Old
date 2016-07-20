using DoorofSoul.Library.General;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLAnswerRepository : AnswerRepository
    {
        public override Answer Find(int answerID, Player correspondingPlayer)
        {
            string sqlString = @"SELECT  
                AnswerID
                from Answers WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Answer(answerID, correspondingPlayer);
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
                
                WHERE AnswerID = @answerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@answerID", answer.AnswerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLAnswerRepository Save Answer Error from PlayerID:{0}, IPAddress:{1}", answer.Player.PlayerID, answer.Player.LastConnectedIPAddress);
                }
            }
        }
    }
}
