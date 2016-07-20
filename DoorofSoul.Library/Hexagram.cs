using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Library
{
    public class Hexagram
    {
        public Dictionary<int, Answer> AnswerDictionary;
        public Dictionary<int, Soul> SoulDictionary;
        public Dictionary<int, Container> ContainerDictionary;
        public Dictionary<int, Entity> EntityDictionary;
        public Dictionary<int, World> WorldDictionary;
        public Dictionary<int, Scene> SceneDictionary;

        public Hexagram()
        {
            AnswerDictionary = new Dictionary<int, Answer>();
            SoulDictionary = new Dictionary<int, Soul>();
            ContainerDictionary = new Dictionary<int, Container>();
            EntityDictionary = new Dictionary<int, Entity>();
            WorldDictionary = new Dictionary<int, World>();
            SceneDictionary = new Dictionary<int, Scene>();
        }

        public void ProjectAnswer(Answer answer)
        {
            if(!AnswerDictionary.ContainsKey(answer.AnswerID))
            {
                AnswerDictionary.Add(answer.AnswerID, answer);
            }
            foreach(Soul soul in answer.Souls)
            {
                if(!SoulDictionary.ContainsKey(soul.SoulID))
                {
                    SoulDictionary.Add(soul.SoulID, soul);
                }
            }
            foreach(Container container in answer.Containers)
            {
                if (!ContainerDictionary.ContainsKey(container.ContainerID))
                {
                    ContainerDictionary.Add(container.ContainerID, container);
                }
                if (!EntityDictionary.ContainsKey(container.EntityID))
                {
                    EntityDictionary.Add(container.EntityID, container);
                }
            }
            foreach(World world in WorldDictionary.Values)
            {
                foreach (Container container in answer.Containers)
                {
                    world.EntityEnter(container);
                }
            }
        }
        public void ExtractAnswer(Answer answer)
        {
            if (AnswerDictionary.ContainsKey(answer.AnswerID))
            {
                AnswerDictionary.Remove(answer.AnswerID);
            }
            foreach (Soul soul in answer.Souls)
            {
                if (SoulDictionary.ContainsKey(soul.SoulID))
                {
                    SoulDictionary.Remove(soul.SoulID);
                }
            }
            foreach (Container container in answer.Containers)
            {
                if (ContainerDictionary.ContainsKey(container.ContainerID))
                {
                    ContainerDictionary.Remove(container.ContainerID);
                }
                if (EntityDictionary.ContainsKey(container.EntityID))
                {
                    EntityDictionary.Remove(container.EntityID);
                }
            }
            foreach (World world in WorldDictionary.Values)
            {
                foreach (Container container in answer.Containers)
                {
                    world.EntityExit(container);
                }
            }
        }

    }
}
