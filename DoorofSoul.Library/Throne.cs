using DoorofSoul.Database;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Library
{
    public class Throne
    {
        private Dictionary<int, Answer> answerDictionary;
        private Dictionary<int, Soul> soulDictionary;

        public Throne()
        {
            answerDictionary = new Dictionary<int, Answer>();
            soulDictionary = new Dictionary<int, Soul>();
        }

        public void ProjectPlayer(Player player)
        {
            Answer answer = DataBase.Instance.RepositoryManager.ThroneList.AnswerRepository.Find(player.AnswerID, player);
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
                answer.LoadSouls(DataBase.Instance.RepositoryManager.ThroneList.SoulRepository.ListOfAnswer(answer));
                foreach (Soul soul in answer.Souls)
                {
                    soul.BindSkillKnowledgeInterface(new HeptagramSkillKnowledgeInterface());

                    soul.Attributes.OnCorePointChange += soul.SoulEventManager.InformDataResolver.InformCorePointChange;
                    soul.Attributes.OnSpiritPointChange += soul.SoulEventManager.InformDataResolver.InformSpiritPointChange;

                    soul.SkillLibrary.LoadSkillInfos(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.SkillInfoRepository.ListOfUnderstander(soul.SoulID));
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
                    if (Hexagram.Instance.Nature.ContainsContainer(containerID))
                    {
                        container = Hexagram.Instance.Nature.FindContainer(containerID);
                    }
                    else
                    {
                        container = DataBase.Instance.RepositoryManager.NatureList.ContainerRepository.Find(containerID);

                        container.Attributes.OnLifePointChange += container.ContainerEventManager.InformDataResolver.InformLifePointChange;
                        container.Attributes.OnEnergyPointChange += container.ContainerEventManager.InformDataResolver.InformEnergyPointChange;
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
                    Hexagram.Instance.Nature.ProjectContainer(container);
                }
                return true;
            }
            else
            {
                return false;
            }
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
                DataBase.Instance.RepositoryManager.ThroneList.AnswerRepository.Save(answer);
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
                DataBase.Instance.RepositoryManager.ThroneList.SoulRepository.Save(soul);
                foreach (Container container in soul.Containers)
                {
                    container.UnlinkSoul(soul);
                    if (container.IsEmptyContainer)
                    {
                        DataBase.Instance.RepositoryManager.NatureList.ContainerRepository.Save(container);
                        Hexagram.Instance.Nature.ExtractContainer(container);
                        container.Attributes.OnLifePointChange -= container.ContainerEventManager.InformDataResolver.InformLifePointChange;
                        container.Attributes.OnEnergyPointChange -= container.ContainerEventManager.InformDataResolver.InformEnergyPointChange;
                    }
                }
                soul.UnlinkAllContainers();
                DeactiveSoul(soul.SoulID);
                soulDictionary.Remove(soul.SoulID);
                soul.Attributes.OnCorePointChange -= soul.SoulEventManager.InformDataResolver.InformCorePointChange;
                soul.Attributes.OnSpiritPointChange -= soul.SoulEventManager.InformDataResolver.InformSpiritPointChange;
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

        public bool CreateSoul(int answerID, string soulName, SoulKernelTypeCode mainSoulType)
        {
            if(answerDictionary.ContainsKey(answerID))
            {
                Answer answer = answerDictionary[answerID];
                Soul soul = DataBase.Instance.RepositoryManager.ThroneList.SoulRepository.Create(answer, soulName, mainSoulType);
                answer.LoadSouls(new List<Soul> { soul });
                Container defaultContainer = DataBase.Instance.RepositoryManager.NatureList.ContainerRepository.Create("TestContainer", 1, new EntitySpaceProperties
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
                    DataBase.Instance.RepositoryManager.ThroneList.SoulRepository.Delete(soulID);
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
