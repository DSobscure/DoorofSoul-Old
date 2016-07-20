using System;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace DoorofSoul.Database.DatabaseElements.Authentications.MySQL
{
    public class MySQLPlayerAuthentication : PlayerAuthentication
    {
        public override bool LoginCheck(string account, string password)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT 1 FROM players WHERE Account = @account and PasswordHash = @passwordHash;", DataBase.Instance.Connection as MySqlConnection))
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
    }
}
