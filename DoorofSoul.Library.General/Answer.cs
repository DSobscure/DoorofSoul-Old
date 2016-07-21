using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Answer
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

        public Answer(int answerID, int soulCountLimit, Player player)
        {
            AnswerID = answerID;
            SoulCountLimit = soulCountLimit;
            Player = player;
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
        }
        public void ClearSouls()
        {
            soulDictionary.Clear();
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
            }
        }

        public void ClearContainers()
        {
            containerDictionary.Clear();
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
            }
        }
    }
}
