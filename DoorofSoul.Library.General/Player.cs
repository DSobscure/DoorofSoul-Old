using DoorofSoul.Protocol.Communication;
using System.Net;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Library.General
{
    public class Player
    {
        public int PlayerID { get; protected set; }
        public string Account { get; protected set; }
        public string Nickname { get; protected set; }
        public SupportLauguages UsingLanguage { get; protected set; }
        public IPAddress LastConnectedIPAddress { get; protected set; }
        public int AnswerID { get; protected set; }
        public bool IsOnline { get; set; }
        public bool IsActivated { get; set; }
        public Answer Answer { get; protected set; }

        public Player()
        {
            UsingLanguage = SupportLauguages.Chinese_Traditional;
        }
        public Player(int playerID, string account, string nickname, SupportLauguages usingLanguage, IPAddress lastConnectedIPAddress, int answerID)
        {
            PlayerID = playerID;
            Account = account;
            Nickname = nickname;
            UsingLanguage = usingLanguage;
            LastConnectedIPAddress = lastConnectedIPAddress;
            AnswerID = answerID;
        }

        public void LoadPlayer(int playerID, string account, string nickname, SupportLauguages usingLanguage, IPAddress lastConnectedIPAddress, int answerID)
        {
            PlayerID = playerID;
            Account = account;
            Nickname = nickname;
            UsingLanguage = usingLanguage;
            LastConnectedIPAddress = lastConnectedIPAddress;
            AnswerID = answerID;
        }
        public void LoadPlayer(Player player)
        {
            PlayerID = player.PlayerID;
            Account = player.Account;
            Nickname = player.Nickname;
            UsingLanguage = player.UsingLanguage;
            LastConnectedIPAddress = player.LastConnectedIPAddress;
            AnswerID = player.AnswerID;
        }

        public Action<EventCode, Dictionary<byte, object>> SendEvent { get; protected set; }

        public bool ActiveAnswer(Answer answer)
        {
            if(answer.AnswerID == AnswerID)
            {
                Answer = answer;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
