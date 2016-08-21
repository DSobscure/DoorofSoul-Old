using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Language;
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLPlayerRepository : PlayerRepository
    {
        public override bool Contains(string account, out int playerID)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT PlayerID FROM PlayerCollection WHERE Account = @account;", Database.ConnectionList.Connection as MySqlConnection))
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

        public override PlayerData Find(int playerID)
        {
            string sqlString = @"SELECT  
                Account, Nickname, UsingLanguage, LastConnectedIPAddress, AnswerID
                from PlayerCollection WHERE PlayerID = @playerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.Connection as MySqlConnection))
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
                        return new PlayerData { playerID = playerID, account = account, nickname = nickname, usingLanguage = usingLanguage, lastConnectedIPAddress = lastConnectedIPAddress, answerID = answerID };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public override bool LoginCheck(string account, string password)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT 1 FROM PlayerCollection WHERE Account = @account and PasswordHash = @passwordHash;", Database.ConnectionList.Connection as MySqlConnection))
            {
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                string passwordHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(password)));

                command.Parameters.AddWithValue("@account", account);
                command.Parameters.AddWithValue("@passwordHash", passwordHash);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override void Save(Player player)
        {
            string sqlString = @"UPDATE PlayerCollection SET 
                UsingLanguage = @usingLanguage,
                LastConnectedIPAddress = @lastConnectedIPAddress
                WHERE PlayerID = @playerID;";
            using (MySqlCommand command = new MySqlCommand(sqlString, Database.ConnectionList.Connection as MySqlConnection))
            {
                command.Parameters.AddWithValue("@usingLanguage", (byte)player.UsingLanguage);
                command.Parameters.AddWithValue("@lastConnectedIPAddress", player.LastConnectedIPAddress.ToString());
                command.Parameters.AddWithValue("@playerID", player.PlayerID);
                if (command.ExecuteNonQuery() <= 0)
                {
                    Database.Log.ErrorFormat("MySQLPlayerRepository Save Player Error from PlayerID:{0}, IPAddress:{1}", player.PlayerID, player.LastConnectedIPAddress);
                }
            }
        }
    }
}
