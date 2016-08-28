using DoorofSoul.Library.General.LightComponents.Communications.Events.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library.General.ThroneComponents
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

        private event Action<List<Soul>> onLoadSouls;
        public event Action<List<Soul>> OnLoadSouls { add { onLoadSouls += value; } remove { onLoadSouls -= value; } }
        private event Action<List<Container>> onLoadContainers;
        public event Action<List<Container>> OnLoadContainers { add { onLoadContainers += value; } remove { onLoadContainers -= value; } }
        #endregion

        #region communication
        public AnswerEventManager AnswerEventManager { get; set; }
        public AnswerOperationManager AnswerOperationManager { get; set; }
        internal AnswerResponseManager AnswerResponseManager { get; set; }
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
            onLoadSouls?.Invoke(new List<Soul>());
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
                        onLoadSouls?.Invoke(new List<Soul> { soul });
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
                onLoadSouls?.Invoke(Souls.ToList());
            }
        }

        public void ClearContainers()
        {
            containerDictionary.Clear();
            ClearIncompleteSoulIDContainerIDConnection();
            onLoadContainers?.Invoke(new List<Container>());
        }
        public virtual void LoadContainers(List<Container> containers)
        {
            foreach(Container container in containers)
            {
                if(!containerDictionary.ContainsKey(container.ContainerID))
                {
                    containerDictionary.Add(container.ContainerID, container);
                    onLoadContainers?.Invoke(new List<Container> { container });
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
                onLoadContainers?.Invoke(Containers.ToList());
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
