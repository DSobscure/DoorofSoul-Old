﻿using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Player;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Events.Managers;

namespace DoorofSoul.Library.General
{
    public abstract class Answer
    {
        public int AnswerID { get; protected set; }
        public Player Player { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }
        public int SoulCount { get { return soulDictionary.Count; } }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        public int ContainerCount { get { return containerDictionary.Count; } }
        public int SoulCountLimit { get; protected set; }

        private event Action<Answer> onLoadSouls;
        public event Action<Answer> OnLoadSouls { add { onLoadSouls += value; } remove { onLoadSouls -= value; } }
        private event Action<Answer> onLoadContainers;
        public event Action<Answer> OnLoadContainers { add { onLoadContainers += value; } remove { onLoadContainers -= value; } }

        #region communication
        public AnswerEventManager AnswerEventManager { get; protected set; }
        public AnswerOperationManager AnswerOperationManager { get; protected set; }
        public void SendEvent(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)AnswerEventParameterCode.AnswerID, AnswerID },
                { (byte)AnswerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)AnswerEventParameterCode.Parameters, parameters }
            };
            Player.SendEvent(PlayerEventCode.AnswerEvent, eventData);
        }
        public void SendResponse(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerOperationParameterCode.Parameters, parameters }
            };
            Player.SendResponse(PlayerOperationCode.AnswerOperation, operationData);
        }
        public void SendError(AnswerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerOperationParameterCode.Parameters, parameters }
            };
            Player.SendError(PlayerOperationCode.AnswerOperation, errorCode, debugMessage, operationData);
        }

        public abstract bool DeleteSoul(int soulID);
        public abstract bool CreateSoul(string soulName);
        public abstract bool ActiveSoul(int soulID);
        #endregion

        public Answer(int answerID, int soulCountLimit, Player player)
        {
            AnswerID = answerID;
            SoulCountLimit = soulCountLimit;
            Player = player;
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
            AnswerOperationManager = new AnswerOperationManager(this);
            AnswerEventManager = new AnswerEventManager(this);
        }

        public void ClearSouls()
        {
            soulDictionary.Clear();
            onLoadSouls?.Invoke(this);
        }
        public virtual void LoadSouls(List<Soul> souls)
        {
            if(SoulCount + souls.Count <= SoulCountLimit)
            {
                foreach (Soul soul in souls)
                {
                    if (!soulDictionary.ContainsKey(soul.SoulID) && soul.AnswerID == AnswerID)
                    {
                        soulDictionary.Add(soul.SoulID, soul);
                    }
                }
                onLoadSouls?.Invoke(this);
            }
        }
        public bool ContainsSoul(int soulID)
        {
            return soulDictionary.ContainsKey(soulID);
        }
        public Soul FindSoul(int soulID)
        {
            if (ContainsSoul(soulID))
            {
                return soulDictionary[soulID];
            }
            else
            {
                return null;
            }
        }
        public void RemoveSoul(int soulID)
        {
            if(soulDictionary.ContainsKey(soulID))
            {
                soulDictionary.Remove(soulID);
                onLoadSouls?.Invoke(this);
            }
        }

        public void ClearContainers()
        {
            containerDictionary.Clear();
            onLoadContainers?.Invoke(this);
        }
        public virtual void LoadContainers(List<Container> containers)
        {
            foreach(Container container in containers)
            {
                if(!containerDictionary.ContainsKey(container.ContainerID))
                {
                    containerDictionary.Add(container.ContainerID, container);
                }
            }
            onLoadContainers?.Invoke(this);
        }
        public bool ContainsContainer(int containerID)
        {
            return containerDictionary.ContainsKey(containerID);
        }
        public Container FindContainer(int containerID)
        {
            if (ContainsContainer(containerID))
            {
                return containerDictionary[containerID];
            }
            else
            {
                return null;
            }
        }
        public void RemoveContainer(int containerID)
        {
            if (containerDictionary.ContainsKey(containerID))
            {
                containerDictionary.Remove(containerID);
                onLoadContainers?.Invoke(this);
            }
        }
    }
}
