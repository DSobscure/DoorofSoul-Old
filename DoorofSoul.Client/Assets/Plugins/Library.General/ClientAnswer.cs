using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Library.General
{
    public class ClientAnswer : Answer
    {
        private Dictionary<int,HashSet<int>> incompleteSoulIDContainerIDConnection_SoulKey;
        private Dictionary<int, HashSet<int>> incompleteSoulIDContainerIDConnection_ContainerKey;

        public ClientAnswer(int answerID, int soulCountLimit, Player player) : base(answerID, soulCountLimit, player)
        {
            incompleteSoulIDContainerIDConnection_SoulKey = new Dictionary<int, HashSet<int>>();
            incompleteSoulIDContainerIDConnection_ContainerKey = new Dictionary<int, HashSet<int>>();
        }

        public override void LoadSouls(List<Soul> souls)
        {
            base.LoadSouls(souls);
            foreach(Soul soul in souls)
            {
                if(incompleteSoulIDContainerIDConnection_SoulKey.ContainsKey(soul.SoulID))
                {
                    foreach(int containerID in incompleteSoulIDContainerIDConnection_SoulKey[soul.SoulID])
                    {
                        if(ContainsContainer(containerID))
                        {
                            Container container = containerDictionary[containerID];
                            soul.LinkContainer(container);
                            container.LinkSoul(soul);
                        }
                    }
                }
            }
        }
        public override void LoadContainers(List<Container> containers)
        {
            base.LoadContainers(containers);
            foreach (Container container in containers)
            {
                if (incompleteSoulIDContainerIDConnection_ContainerKey.ContainsKey(container.ContainerID))
                {
                    foreach (int soulID in incompleteSoulIDContainerIDConnection_ContainerKey[container.ContainerID])
                    {
                        if (ContainsSoul(soulID))
                        {
                            Soul soul = soulDictionary[soulID];
                            soul.LinkContainer(container);
                            container.LinkSoul(soul);
                        }
                    }
                }
            }
        }
        public void RecordIncompleteSoulIDContainerIDConnection(int soulID, int containerID)
        {
            if(incompleteSoulIDContainerIDConnection_SoulKey.ContainsKey(soulID))
            {
                incompleteSoulIDContainerIDConnection_SoulKey[soulID].Add(containerID);
            }
            else
            {
                incompleteSoulIDContainerIDConnection_SoulKey.Add(soulID, new HashSet<int> { containerID });
            }

            if (incompleteSoulIDContainerIDConnection_ContainerKey.ContainsKey(containerID))
            {
                incompleteSoulIDContainerIDConnection_ContainerKey[containerID].Add(soulID);
            }
            else
            {
                incompleteSoulIDContainerIDConnection_ContainerKey.Add(containerID, new HashSet<int> { soulID });
            }
        }
        public void ClearIncompleteSoulIDContainerIDConnection()
        {
            incompleteSoulIDContainerIDConnection_SoulKey.Clear();
            incompleteSoulIDContainerIDConnection_ContainerKey.Clear();
        }

        public override bool DeleteSoul(int soulID)
        {
            throw new NotImplementedException();
        }

        public override bool CreateSoul(string soulName)
        {
            throw new NotImplementedException();
        }

        public override bool ActiveSoul(int soulID)
        {
            throw new NotImplementedException();
        }
    }
}
