using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class ContainerStatusEffectManager
    {
        protected Container container;
        protected Dictionary<int, ContainerStatusEffectInfo> statusEffectInfoDictionary;

        public IEnumerable<ContainerStatusEffectInfo> StatusEffectInfos { get { return statusEffectInfoDictionary.Values; } }

        private event Action<ContainerStatusEffectInfo, bool> onContainerStatusEffectInfoChange;
        public event Action<ContainerStatusEffectInfo, bool> OnContainerStatusEffectInfoChange { add { onContainerStatusEffectInfoChange += value; } remove { onContainerStatusEffectInfoChange -= value; } }

        public ContainerStatusEffectManager(Container container)
        {
            this.container = container;
            statusEffectInfoDictionary = new Dictionary<int, ContainerStatusEffectInfo>();
        }

        public bool ContainsStatusEffectInfo(int statusEffectInfoID)
        {
            return statusEffectInfoDictionary.ContainsKey(statusEffectInfoID);
        }

        public ContainerStatusEffectInfo FindStatusEffectInfo(int statusEffectInfoID)
        {
            if(ContainsStatusEffectInfo(statusEffectInfoID))
            {
                return statusEffectInfoDictionary[statusEffectInfoID];
            }
            else
            {
                return null;
            }
        }

        public void LoadStatusEffectInfo(ContainerStatusEffectInfo info)
        {
            if(info.AffectedContainerID == container.ContainerID)
            {
                if (!ContainsStatusEffectInfo(info.ContainerStatusEffectInfoID))
                {
                    statusEffectInfoDictionary.Add(info.ContainerStatusEffectInfoID, info);
                }
                else
                {
                    statusEffectInfoDictionary[info.ContainerStatusEffectInfoID] = info;
                }
                onContainerStatusEffectInfoChange?.Invoke(info, true);
            }
        }
        public void LoadStatusEffectInfos(List<ContainerStatusEffectInfo> infos)
        {
            foreach(ContainerStatusEffectInfo info in infos)
            {
                LoadStatusEffectInfo(info);
            }
        }
        public void UnloadStatusEffectInfo(ContainerStatusEffectInfo info)
        {
            if (info.AffectedContainerID == container.ContainerID)
            {
                if (ContainsStatusEffectInfo(info.ContainerStatusEffectInfoID))
                {
                    onContainerStatusEffectInfoChange?.Invoke(info, false);
                    statusEffectInfoDictionary.Remove(info.ContainerStatusEffectInfoID);
                }
            }
        }
        public void UnloadAllStatusEffectInfo()
        {
            foreach(ContainerStatusEffectInfo info in StatusEffectInfos)
            {
                onContainerStatusEffectInfoChange?.Invoke(info, false);
                statusEffectInfoDictionary.Remove(info.ContainerStatusEffectInfoID);
            }
        }
    }
}
