using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Player;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
{
    public class Answer
    {
        #region properties
        public int AnswerID { get; protected set; }
        public Player Player { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }
        public int SoulCount { get { return soulDictionary.Count; } }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        public int ContainerCount { get { return containerDictionary.Count; } }
        public int SoulCountLimit { get; protected set; }
        public SupportLauguages UsingLanguage { get { return Player.UsingLanguage; } }
        private Dictionary<int, HashSet<int>> incompleteSoulIDContainerIDConnection_SoulKey;
        private Dictionary<int, HashSet<int>> incompleteSoulIDContainerIDConnection_ContainerKey;

        private event Action<Answer> onLoadSouls;
        public event Action<Answer> OnLoadSouls { add { onLoadSouls += value; } remove { onLoadSouls -= value; } }
        private event Action<Answer> onLoadContainers;
        public event Action<Answer> OnLoadContainers { add { onLoadContainers += value; } remove { onLoadContainers -= value; } }
        #endregion
        #region communication
        internal AnswerEventManager AnswerEventManager { get; set; }
        internal AnswerOperationManager AnswerOperationManager { get; set; }
        internal AnswerResponseManager AnswerResponseManager { get; set; }
        internal void SendEvent(AnswerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)AnswerEventParameterCode.AnswerID, AnswerID },
                { (byte)AnswerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)AnswerEventParameterCode.Parameters, parameters }
            };
            Player.SendEvent(PlayerEventCode.AnswerEvent, eventData);
        }
        internal void SendOperation(AnswerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)AnswerOperationParameterCode.AnswerID, AnswerID },
                { (byte)AnswerOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerOperationParameterCode.Parameters, parameters }
            };
            Player.SendOperation(PlayerOperationCode.AnswerOperation, operationData);
        }
        internal void SendResponse(AnswerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)AnswerResponseParameterCode.AnswerID, AnswerID },
                { (byte)AnswerResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)AnswerResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)AnswerResponseParameterCode.DebugMessage, debugMessage },
                { (byte)AnswerResponseParameterCode.Parameters, parameters }
            };
            Player.SendResponse(PlayerOperationCode.AnswerOperation, ErrorCode.NoError, null, responseData);
        }
        internal void ErrorInform(string title, string message)
        {
            Player.ErrorInform(title, message);
        }

        public bool DeleteSoul(int soulID)
        {
            return Player.DeleteSoul(this, soulID);
        }
        public bool CreateSoul(string soulName)
        {
            return Player.CreateSoul(this, soulName);
        }
        public bool ActivateSoul(int soulID)
        {
            return Player.ActivateSoul(this, soulID);
        }
        public void FetchSouls()
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)AnswerFetchDataCode.Souls },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(AnswerOperationCode.FetchData, fetchDataParameters);
        }
        public void FetchContainers()
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)AnswerFetchDataCode.Containers },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(AnswerOperationCode.FetchData, fetchDataParameters);
        }
        public void FetchSoulContainerLinks()
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)AnswerFetchDataCode.SoulContainerLinks },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(AnswerOperationCode.FetchData, fetchDataParameters);
        }
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
            AnswerResponseManager = new AnswerResponseManager(this);
            incompleteSoulIDContainerIDConnection_SoulKey = new Dictionary<int, HashSet<int>>();
            incompleteSoulIDContainerIDConnection_ContainerKey = new Dictionary<int, HashSet<int>>();
        }

        public void ClearSouls()
        {
            soulDictionary.Clear();
            ClearIncompleteSoulIDContainerIDConnection();
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
                foreach (Soul soul in souls)
                {
                    if (incompleteSoulIDContainerIDConnection_SoulKey.ContainsKey(soul.SoulID))
                    {
                        foreach (int containerID in incompleteSoulIDContainerIDConnection_SoulKey[soul.SoulID])
                        {
                            if (ContainsContainer(containerID))
                            {
                                Container container = containerDictionary[containerID];
                                soul.LinkContainer(container);
                                container.LinkSoul(soul);
                            }
                        }
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
            ClearIncompleteSoulIDContainerIDConnection();
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
            foreach (Container container in containers)
            {
                if (incompleteSoulIDContainerIDConnection_ContainerKey.ContainsKey(container.ContainerID))
                {
                    foreach (int soulID in incompleteSoulIDContainerIDConnection_ContainerKey[container.ContainerID])
                    {
                        if (ContainsSoul(soulID))
                        {
                            Soul soul = soulDictionary[soulID];
                            soul.LinkContainer(container);
                            container.LinkSoul(soul);
                        }
                    }
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
        public void LinkSoulContainer(int soulID, int containerID)
        {
            if(ContainsSoul(soulID) && ContainsContainer(containerID))
            {
                Soul soul = FindSoul(soulID);
                Container container = FindContainer(containerID);
                soul.LinkContainer(container);
                container.LinkSoul(soul);
            }
            else
            {
                RecordIncompleteSoulIDContainerIDConnection(soulID, containerID);
            }
        }
        private void RecordIncompleteSoulIDContainerIDConnection(int soulID, int containerID)
        {
            if (incompleteSoulIDContainerIDConnection_SoulKey.ContainsKey(soulID))
            {
                incompleteSoulIDContainerIDConnection_SoulKey[soulID].Add(containerID);
            }
            else
            {
                incompleteSoulIDContainerIDConnection_SoulKey.Add(soulID, new HashSet<int> { containerID });
            }

            if (incompleteSoulIDContainerIDConnection_ContainerKey.ContainsKey(containerID))
            {
                incompleteSoulIDContainerIDConnection_ContainerKey[containerID].Add(soulID);
            }
            else
            {
                incompleteSoulIDContainerIDConnection_ContainerKey.Add(containerID, new HashSet<int> { soulID });
            }
        }
        private void ClearIncompleteSoulIDContainerIDConnection()
        {
            incompleteSoulIDContainerIDConnection_SoulKey.Clear();
            incompleteSoulIDContainerIDConnection_ContainerKey.Clear();
        }
    }
}
