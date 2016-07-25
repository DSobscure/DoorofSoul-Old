﻿using DoorofSoul.Library.General.Events;
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
    public abstract class Player
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
        public PlayerEventManager PlayerEventManager { get; protected set; }
        public PlayerOperationManager PlayerOperationManager { get; protected set; }
        public abstract void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendResponse(PlayerOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendError(PlayerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters);
        public abstract void SendWorldEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendWorldResponse(WorldOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendWorldError(WorldOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters);

        public abstract bool Login(string account, string password, out string debugMessage, out string errorMessage);
        public abstract void Logout();
        public abstract void FetchSystemVersion(out string serverVersion, out string clientVersion);
        public abstract void FetchAnswer(out Answer answer);
        public abstract void FetchScene(int sceneID, out Scene scene);
        #endregion

        public Player()
        {
            UsingLanguage = SupportLauguages.Chinese_Traditional;
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
