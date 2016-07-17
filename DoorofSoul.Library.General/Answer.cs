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
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }

        public Answer(int answerID, Player player)
        {
            AnswerID = answerID;
            Player = player;
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
        }
        public void LoadSouls(List<Soul> souls)
        {
            foreach(Soul soul in souls)
            {
                if(!soulDictionary.ContainsKey(soul.SoulID))
                {
                    soulDictionary.Add(soul.SoulID, soul);
                }
            }
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
        }
    }
}
