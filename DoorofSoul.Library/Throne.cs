using DoorofSoul.Database;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.EntityElements;
using DoorofSoul.Library.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Protocol;
using System.Collections.Generic;

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
                answer.LoadSouls(DataBase.Instance.RepositoryManager.SoulRepository.ListOfAnswer(answer));
                foreach (Soul soul in answer.Souls)
                {
                    soul.BindSkillKnowledgeInterface(new HeptagramSkillKnowledgeInterface());
                    soul.SkillLibrary.LoadSkillInfos(DataBase.Instance.RepositoryManager.SkillInfoRepository.ListOfUnderstander(soul.SoulID));
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
                List<Container> containers = new List<Container>();
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
                        containerDictionary.Add(containerID, container);
                    }
                    soul.LinkContainer(container);
                    container.LinkSoul(soul);
                    containers.Add(container);
                }
                Answer answer = answerDictionary[soul.AnswerID];
                answer.LoadContainers(containers);
            }
        }
        public bool ActiveSoul(int soulID)
        {
            if (soulDictionary.ContainsKey(soulID))
            {
                Soul soul = soulDictionary[soulID];
                soul.IsActivate = true;
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
                
            }
            Hexagram.Instance.Nature.ProjectContainer(container);
        }

        public void ExtractPlayer(Player player)
        {
            if (player.Answer != null)
            {
                DataBase.Instance.RepositoryManager.PlayerRepository.Save(player);
                ExtractAnswer(player.Answer);
            }
        }
        protected void ExtractAnswer(Answer answer)
        {
            if (answerDictionary.ContainsKey(answer.AnswerID))
            {
                DataBase.Instance.RepositoryManager.AnswerRepository.Save(answer);
                foreach (Soul soul in answer.Souls)
                {
                    ExtractSoul(soul);

                }
                answer.ClearSouls();
                answerDictionary.Remove(answer.AnswerID);
            }
        }
        protected void ExtractSoul(Soul soul)
        {
            if (soulDictionary.ContainsKey(soul.SoulID))
            {
                DataBase.Instance.RepositoryManager.SoulRepository.Save(soul);
                foreach (Container container in soul.Containers)
                {
                    container.UnlinkSoul(soul);
                    if(soul.IsActivate)
                    {
                        Scene locatedScene = container.Entity.LocatedScene;
                        if (container.IsEmptyContainer)
                        {
                            ExtractContainer(container);
                        }
                    }
                }
                soul.UnlinkAllContainers();
                DeactiveSoul(soul.SoulID);
                soulDictionary.Remove(soul.SoulID);
            }
        }
        public bool DeactiveSoul(int soulID)
        {
            if (soulDictionary.ContainsKey(soulID))
            {
                soulDictionary[soulID].IsActivate = false;
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
                DataBase.Instance.RepositoryManager.ContainerRepository.Save(container);
                Hexagram.Instance.Nature.ExtractContainer(container);
                containerDictionary.Remove(container.ContainerID);
            }
        }

        public bool CreateSoul(int answerID, string soulName, SoulKernelTypeCode mainSoulType)
        {
            if(answerDictionary.ContainsKey(answerID))
            {
                Answer answer = answerDictionary[answerID];
                Soul soul = DataBase.Instance.RepositoryManager.SoulRepository.Create(answer, soulName, mainSoulType);
                answer.LoadSouls(new List<Soul> { soul });
                Container defaultContainer = DataBase.Instance.RepositoryManager.ContainerRepository.Create("TestContainer", 1, new EntitySpaceProperties
                {
                    Scale = new DSVector3 { x = 1, y = 1, z = 1 },
                });
                DataBase.Instance.RelationManager.SoulID_ContainerID_Relation.Link_Soul_Container(soul.SoulID, defaultContainer.ContainerID);
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
