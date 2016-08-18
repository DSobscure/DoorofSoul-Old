using DoorofSoul.Library.General.LightComponents.Communications.Events.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol.Language;
using System;
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

        private bool isOnline;
        public bool IsOnline
        {
            get { return isOnline; }
            set
            {
                isOnline = value;
                onOnline?.Invoke(this);
            }
        }

        private bool isActived;
        public bool IsActivated
        {
            get { return isActived; }
            set
            {
                isActived = value;
                onActiveAnswer?.Invoke(Answer);
            }
        }
        public Answer Answer { get; protected set; }
        #endregion

        #region events
        private event Action<Answer> onActiveAnswer;
        public event Action<Answer> OnActiveAnswer { add { onActiveAnswer += value; } remove { onActiveAnswer -= value; } }

        #region OnOnline
        private event Action<Player> onOnline;
        public event Action<Player> OnOnline
        {
            add { onOnline += value; }
            remove { onOnline -= value; }
        }
        #endregion

        #endregion

        #region communication
        public PlayerEventManager PlayerEventManager { get; protected set; }
        public PlayerOperationManager PlayerOperationManager { get; protected set; }
        public PlayerResponseManager PlayerResponseManager { get; protected set; }
        public PlayerCommunicationInterface PlayerCommunicationInterface { get; protected set; }
        #endregion

        public Player(PlayerCommunicationInterface communicationInterface)
        {
            UsingLanguage = SupportLauguages.Chinese_Traditional;
            PlayerEventManager = new PlayerEventManager(this);
            PlayerOperationManager = new PlayerOperationManager(this);
            PlayerResponseManager = new PlayerResponseManager(this);
            PlayerCommunicationInterface = communicationInterface;
            PlayerCommunicationInterface.BindPlayer(this);
        }
        public void LoadPlayer(int playerID, string account, string nickname, SupportLauguages usingLanguage, int answerID)
        {
            PlayerID = playerID;
            Account = account;
            Nickname = nickname;
            UsingLanguage = usingLanguage;
            AnswerID = answerID;
        }

        public bool ActiveAnswer(Answer answer)
        {
            if(answer.AnswerID == AnswerID)
            {
                Answer = answer;
                IsActivated = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
