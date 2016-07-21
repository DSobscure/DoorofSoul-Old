using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DoorofSoul.Library.General
{
    public class Container : Entity
    {
        public int ContainerID { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public bool IsEmptyContainer { get { return soulDictionary.Count == 0; } }

        public Container(int containerID, int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties) : base(entityID, entityName, locatedSceneID, spaceProperties)
        {
            ContainerID = containerID;
            soulDictionary = new Dictionary<int, Soul>();
        }

        public void LinkSoul(Soul soul)
        {
            if(!soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Add(soul.SoulID, soul);
            }
        }
        public void UnlinkSoul(Soul soul)
        {
            if (soulDictionary.ContainsKey(soul.SoulID))
            {
                soulDictionary.Remove(soul.SoulID);
            }
        }
    }
}
