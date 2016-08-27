using System;
using System.Collections.Generic;
using System.Linq;
using DoorofSoul.Protocol;
using System.Threading.Tasks;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.ThroneComponents;

namespace DoorofSoul.Hexagram.ThroneComponents
{
    public class SoulManager
    {
        private Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }

        public SoulManager()
        {
            soulDictionary = new Dictionary<int, Soul>();
        }
        public void Initial()
        {

        }

        public bool ContainsSoul(int soulID)
        {
            return soulDictionary.ContainsKey(soulID);
        }
        public Soul FindSoul(int soulID)
        {
            if(ContainsSoul(soulID))
            {
                return soulDictionary[soulID];
            }
            else
            {
                return null;
            }
        }
        public void ProjectSoul(Soul soul)
        {
            if (!soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Add(soul.SoulID, soul);
                AssemblySoul(soul);
                List<int> containerIDs = Database.Database.RepositoryList.LoveRepositoryList.SoulContainerLinkRepository.GetContainerIDs(soul.SoulID);
                List<Container> containers = new List<Container>();
                foreach (int containerID in containerIDs)
                {
                    Container container;
                    if (Hexagram.Nature.ContainerManager.ContainsContainer(containerID))
                    {
                        container = Hexagram.Nature.ContainerManager.FindContainer(containerID);
                    }
                    else
                    {
                        container = Database.Database.RepositoryList.NatureRepositoryList.ContainerRepository.Find(containerID);
                    }
                    soul.LinkContainer(container);
                    container.LinkSoul(soul);
                    containers.Add(container);
                }
                Answer answer = Hexagram.Throne.AnswerManager.FindAnswer(soul.AnswerID);
                answer.LoadContainers(containers);
            }
        }
        public bool ActiveSoul(int soulID)
        {
            if (soulDictionary.ContainsKey(soulID))
            {
                Soul soul = soulDictionary[soulID];
                soul.IsActivate = true;
                foreach (Container container in soul.Containers)
                {
                    Hexagram.Nature.ContainerManager.ProjectContainer(container);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ExtractSoul(Soul soul)
        {
            if (soulDictionary.ContainsKey(soul.SoulID))
            {
                Database.Database.RepositoryList.ThroneRepositoryList.SoulRepository.Save(soul);
                foreach (Container container in soul.Containers)
                {
                    container.UnlinkSoul(soul);
                    if (container.IsEmptyContainer)
                    {
                        Hexagram.Nature.ContainerManager.ExtractContainer(container);
                    }
                }
                soul.UnlinkAllContainers();
                DeactiveSoul(soul.SoulID);
                soulDictionary.Remove(soul.SoulID);
                DisassemblySoul(soul);
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
            if (Hexagram.Throne.AnswerManager.ContainsAnswer(answerID))
            {
                Answer answer = Hexagram.Throne.AnswerManager.FindAnswer(answerID);
                Soul soul = Database.Database.RepositoryList.ThroneRepositoryList.SoulRepository.Create(answer, soulName, mainSoulType);
                answer.LoadSouls(new List<Soul> { soul });
                Container defaultContainer = Database.Database.RepositoryList.NatureRepositoryList.ContainerRepository.Create("TestContainer", Hexagram.Nature.SceneManager.FirstScene.SceneID, new EntitySpaceProperties
                {
                    Scale = new DSVector3 { x = 1, y = 1, z = 1 },
                });
                Database.Database.RepositoryList.LoveRepositoryList.SoulContainerLinkRepository.Link_Soul_Container(soul.SoulID, defaultContainer.ContainerID);
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
            if (Hexagram.Throne.AnswerManager.ContainsAnswer(answerID))
            {
                Answer answer = Hexagram.Throne.AnswerManager.FindAnswer(answerID);
                if (soulDictionary.ContainsKey(soulID))
                {
                    Soul soul = soulDictionary[soulID];
                    ExtractSoul(soul);
                    Database.Database.RepositoryList.ThroneRepositoryList.SoulRepository.Delete(soulID);
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
        public void AssemblySoul(Soul soul)
        {
            soul.Attributes.OnCorePointChange += soul.SoulEventManager.InformDataResolver.InformCorePointChange;
            soul.Attributes.OnSpiritPointChange += soul.SoulEventManager.InformDataResolver.InformSpiritPointChange;
            soul.SkillLibrary.LoadSkillInfos(Database.Database.RepositoryList.KnowledgeRepositoryList.SkillsRepositoryList.SkillInfoRepository.ListOfUnderstander(soul.SoulID));
        }
        public void DisassemblySoul(Soul soul)
        {
            soul.Attributes.OnCorePointChange -= soul.SoulEventManager.InformDataResolver.InformCorePointChange;
            soul.Attributes.OnSpiritPointChange -= soul.SoulEventManager.InformDataResolver.InformSpiritPointChange;
            foreach (SkillInfo info in soul.SkillLibrary.SkillInfos)
            {
                Database.Database.RepositoryList.KnowledgeRepositoryList.SkillsRepositoryList.SkillInfoRepository.Save(info);
            }
        }
    }
}
