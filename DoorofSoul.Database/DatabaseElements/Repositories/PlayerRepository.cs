using DoorofSoul.Database.Library;
using DoorofSoul.Library.General;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class PlayerRepository
    {
        public abstract bool Register(string account, string password);
        public abstract bool Contains(string account, out int playerID);
        public abstract PlayerData Find(int playerID);
        public abstract void Save(Player player);
        public abstract bool LoginCheck(string account, string password);
        public string HashPassword(string password)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            string passwordHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(password)));
            return passwordHash;
        }
    }
}
