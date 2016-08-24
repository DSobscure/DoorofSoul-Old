using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class ContainerStatusEffectManager
    {
        protected Container container;
        protected Dictionary<int, ContainerStatusEffectInfo> statusEffectInfoDictionary;

        public IEnumerable<ContainerStatusEffectInfo> StatusEffectInfos { get { return statusEffectInfoDictionary.Values; } }

        private event Action<ContainerStatusEffectInfo, DataChangeTypeCode> onContainerStatusEffectInfoChange;
        public event Action<ContainerStatusEffectInfo, DataChangeTypeCode> OnContainerStatusEffectInfoChange { add { onContainerStatusEffectInfoChange += value; } remove { onContainerStatusEffectInfoChange -= value; } }

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
                    onContainerStatusEffectInfoChange?.Invoke(info, DataChangeTypeCode.Load);
                }
                else
                {
                    statusEffectInfoDictionary[info.ContainerStatusEffectInfoID] = info;
                    onContainerStatusEffectInfoChange?.Invoke(info, DataChangeTypeCode.Update);
                }
            }
        }
        public void InitialStatusEffectInfos(List<ContainerStatusEffectInfo> infos)
        {
            foreach(ContainerStatusEffectInfo info in infos)
            {
                if (info.AffectedContainerID == container.ContainerID)
                {
                    if (!ContainsStatusEffectInfo(info.ContainerStatusEffectInfoID))
                    {
                        statusEffectInfoDictionary.Add(info.ContainerStatusEffectInfoID, info);
                    }
                    else
                    {
                        statusEffectInfoDictionary[info.ContainerStatusEffectInfoID] = info;
                    }
                }
            }
            onContainerStatusEffectInfoChange?.Invoke(null, DataChangeTypeCode.Initial);
        }
        public void UnloadStatusEffectInfo(ContainerStatusEffectInfo info)
        {
            if (info.AffectedContainerID == container.ContainerID)
            {
                if (ContainsStatusEffectInfo(info.ContainerStatusEffectInfoID))
                {
                    statusEffectInfoDictionary.Remove(info.ContainerStatusEffectInfoID);
                    onContainerStatusEffectInfoChange?.Invoke(info, DataChangeTypeCode.Unload);
                }
            }
        }
        public void ClearAllStatusEffectInfo()
        {
            statusEffectInfoDictionary.Clear();
            onContainerStatusEffectInfoChange?.Invoke(null, DataChangeTypeCode.ClearAll);
        }
    }
}
