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

        private event Action<Answer> onLoadSouls;
        public event Action<Answer> OnLoadSouls { add { onLoadSouls += value; } remove { onLoadSouls -= value; } }
        private event Action<Answer> onLoadContainers;
        public event Action<Answer> OnLoadContainers { add { onLoadContainers += value; } remove { onLoadContainers -= value; } }

        public Answer(int answerID, Player player)
        {
            AnswerID = answerID;
            Player = player;
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
        }
        public void ClearSouls()
        {
            soulDictionary.Clear();
        }
        public void LoadSouls(List<Soul> souls)
        {
            foreach (Soul soul in souls)
            {
                if(!soulDictionary.ContainsKey(soul.SoulID) && soul.AnswerID == AnswerID)
                {
                    soulDictionary.Add(soul.SoulID, soul);
                }
            }
            onLoadSouls?.Invoke(this);
        }
        public void ClearContainers()
        {
            containerDictionary.Clear();
        }
        public void LoadContainers(List<Container> containers)
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
    }
}
