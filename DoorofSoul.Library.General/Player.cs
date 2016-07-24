using DoorofSoul.Library.General.Events;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;
using System.Net;

namespace DoorofSoul.Library.General
{
    public class Player
    {
        #region properties
        public int PlayerID { get; protected set; }
        public string Account { get; protected set; }
        public string Nickname { get; protected set; }
        public SupportLauguages UsingLanguage { get; protected set; }
        public IPAddress LastConnectedIPAddress { get; protected set; }
        public int AnswerID { get; protected set; }
        public bool IsOnline { get; set; }
        public bool IsActivated { get; set; }
        public Answer Answer { get; protected set; }
        #endregion

        #region events
        private event Action<Answer> onActiveAnswer;
        public event Action<Answer> OnActiveAnswer { add { onActiveAnswer += value; } remove { onActiveAnswer -= value; } }
        #endregion

        #region communication
        public PlayerEventManagers PlayerEventManagers { get; protected set; }
        public PlayerEventManager PlayerEventManager { get; protected set; }
        public PlayerOperationManager PlayerOperationManager { get; protected set; }
        public Action<PlayerEventCode, Dictionary<byte, object>> SendEvent { get; protected set; }
        public Action<PlayerOperationCode, Dictionary<byte, object>> SendResponse { get; protected set; }
        public Action<PlayerOperationCode, ErrorCode, string, Dictionary<byte, object>> SendError { get; protected set; }
        #endregion

        public Player()
        {
            UsingLanguage = SupportLauguages.Chinese_Traditional;
            PlayerEventManager = new PlayerEventManagers(this);
            PlayerEventManager = new PlayerEventManager(this);
            PlayerOperationManager = new PlayerOperationManager(this);
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
            AnswerID = player.AnswerID;
        }

        public bool ActiveAnswer(Answer answer)
        {
            if(answer.AnswerID == AnswerID)
            {
                Answer = answer;
                onActiveAnswer?.Invoke(Answer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
