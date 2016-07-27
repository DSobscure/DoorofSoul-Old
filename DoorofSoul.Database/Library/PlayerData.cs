using DoorofSoul.Protocol.Language;
using System.Net;

namespace DoorofSoul.Database.Library
{
    public class PlayerData
    {
        public int playerID;
        public string account;
        public string nickname;
        public SupportLauguages usingLanguage;
        public IPAddress lastConnectedIPAddress;
        public int answerID;
    }
}
