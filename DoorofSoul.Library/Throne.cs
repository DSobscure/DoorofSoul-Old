using DoorofSoul.Library.General;
using System.Collections.Generic;
using DoorofSoul.Database;

namespace DoorofSoul.Library
{
    public class Throne
    {
        private Dictionary<int, Answer> answerDictionary;
        private Dictionary<int, Soul> soulDictionary;
        private Dictionary<int, Container> containerDictionary;

        public Throne()
        {
            answerDictionary = new Dictionary<int, Answer>();
            soulDictionary = new Dictionary<int, Soul>();
            containerDictionary = new Dictionary<int, Container>();
        }

        public void ProjectPlayer(Player player)
        {
            Answer answer = DataBase.Instance.RepositoryManager.AnswerRepository.Find(player.AnswerID, player);
            if(answer != null)
            {
                ProjectAnswer(answer);
            }
        }
        protected void ProjectAnswer(Answer answer)
        {
            if (!answerDictionary.ContainsKey(answer.AnswerID))
            {
                answerDictionary.Add(answer.AnswerID, answer);
                answer.LoadSouls(DataBase.Instance.RepositoryManager.SoulRepository.ListOfAnswer(answer.AnswerID));
                foreach (Soul soul in answer.Souls)
                {
                    ProjectSoul(soul);
                }
            }
        }
        protected void ProjectSoul(Soul soul)
        {
            if (!soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Add(soul.SoulID, soul);
                List<int> containerIDs = DataBase.Instance.RelationManager.SoulID_ContainerID_Relation.GetContainerIDs(soul.SoulID);
                foreach (int containerID in containerIDs)
                {
                    Container container;
                    if (containerDictionary.ContainsKey(containerID))
                    {
                        container = containerDictionary[containerID];
                    }
                    else
                    {
                        container = DataBase.Instance.RepositoryManager.ContainerRepository.Find(containerID);
                    }
                    soul.LinkContainer(container);
                    container.LinkSoul(soul);
                }
            }
        }
        public bool ActiveSoul(int soulID)
        {
            if (soulDictionary.ContainsKey(soulID))
            {
                Soul soul = soulDictionary[soulID];
                soul.IsActive = true;
                foreach(Container container in soul.Containers)
                {
                    ProjectContainer(container);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ProjectContainer(Container container)
        {
            if (!containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
                Hexagram.Instance.Nature.ProjectEntity(container);
            }
        }

        public void ExtractPlayer(Player player)
        {
            if (player.Answer != null)
            {
                ExtractAnswer(player.Answer);
                DataBase.Instance.RepositoryManager.AnswerRepository.Save(player.Answer);
            }
        }
        protected void ExtractAnswer(Answer answer)
        {
            if (answerDictionary.ContainsKey(answer.AnswerID))
            {
                foreach (Soul soul in answer.Souls)
                {
                    ExtractSoul(soul);
                    DataBase.Instance.RepositoryManager.SoulRepository.Save(soul);
                }
                answer.ClearSouls();
                answerDictionary.Remove(answer.AnswerID);
            }
        }
        protected void ExtractSoul(Soul soul)
        {
            if (soulDictionary.ContainsKey(soul.SoulID))
            {
                foreach (Container container in soul.Containers)
                {
                    container.UnlinkSoul(soul);
                    soul.UnlinkContainer(container);
                    if(container.IsEmptyContainer)
                    {
                        ExtractContainer(container);
                        DataBase.Instance.RepositoryManager.ContainerRepository.Save(container);
                    }
                }
                DeactiveSoul(soul.SoulID);
                soulDictionary.Remove(soul.SoulID);
            }
        }
        public bool DeactiveSoul(int soulID)
        {
            if (soulDictionary.ContainsKey(soulID))
            {
                soulDictionary[soulID].IsActive = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ExtractContainer(Container container)
        {
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                Hexagram.Instance.Nature.ExtractEntity(container);
                containerDictionary.Remove(container.ContainerID);
            }
        }

        public bool CreateSoul(int answerID, string soulName)
        {
            if(answerDictionary.ContainsKey(answerID))
            {
                Answer answer = answerDictionary[answerID];
                Soul soul = DataBase.Instance.RepositoryManager.SoulRepository.Create(answerID, soulName);
                answer.LoadSouls(new List<Soul> { soul });
                ProjectSoul(soul);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteSoul(int answerID, int soulID)
        {
            if(answerDictionary.ContainsKey(answerID))
            {
                Answer answer = answerDictionary[answerID];
                if (soulDictionary.ContainsKey(soulID))
                {
                    Soul soul = soulDictionary[soulID];
                    ExtractSoul(soul);
                    DataBase.Instance.RepositoryManager.SoulRepository.Delete(soulID);
                    answer.RemoveSoul(soulID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
