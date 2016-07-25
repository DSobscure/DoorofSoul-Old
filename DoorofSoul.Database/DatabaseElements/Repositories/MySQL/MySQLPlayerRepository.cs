using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Language;
using MySql.Data.MySqlClient;
using System.Net;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MySQL
{
    public class MySQLPlayerRepository : PlayerRepository
    {
        public override bool Contains(string account, out int playerID)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT PlayerID FROM players WHERE Account = @account;", DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@account", account);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        playerID = reader.GetInt32(0);
                        return true;
                    }
                    else
                    {
                        playerID = -1;
                        return false;
                    }
                }
            }
        }

        public override DatabasePlayer Find(int playerID)
        {
            string sqlString = @"SELECT  
                Account, Nickname, UsingLanguage, LastConnectedIPAddress, AnswerID
                from Players WHERE PlayerID = @playerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@playerID", playerID);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string account = reader.GetString(0);
                        string nickname = reader.IsDBNull(1) ? null : reader.GetString(1);
                        SupportLauguages usingLanguage = (SupportLauguages)reader.GetByte(2);
                        IPAddress lastConnectedIPAddress = reader.IsDBNull(3) ? IPAddress.None : IPAddress.Parse(reader.GetString(3));
                        int answerID = reader.GetInt32(4);
                        return new DatabasePlayer(playerID, account, nickname, usingLanguage, lastConnectedIPAddress, answerID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override void Save(Player player)
        {
            string sqlString = @"UPDATE Players SET 
                UsingLanguage = @usingLanguage,
                LastConnectedIPAddress = @lastConnectedIPAddress
                WHERE PlayerID = @playerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, DataBase.Instance.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@usingLanguage", (byte)player.UsingLanguage);
                command.Parameters.AddWithValue("@lastConnectedIPAddress", player.LastConnectedIPAddress.ToString());
                command.Parameters.AddWithValue("@playerID", player.PlayerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    DataBase.Instance.Log.ErrorFormat("MySQLPlayerRepository Save Player Error from PlayerID:{0}, IPAddress:{1}", player.PlayerID, player.LastConnectedIPAddress);
                }
            }
        }
    }
}
