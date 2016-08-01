using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library.General
{
    public class Container
    {
        #region properties
        public int ContainerID { get; protected set; }
        public int EntityID { get; protected set; }
        public string ContainerName { get; protected set; }
        protected Dictionary<int, Soul> soulDictionary;
        public IEnumerable<Soul> Souls { get { return soulDictionary.Values; } }
        public bool IsEmptyContainer { get { return soulDictionary.Count == 0; } }
        public Soul FirstSoul
        {
            get
            {
                if(IsEmptyContainer)
                {
                    return null;
                } 
                else
                {
                    return Souls.First();
                }
            }
        }
        public Entity Entity { get; protected set; }
        public SupportLauguages UsingLanguage { get { return soulDictionary.FirstOrDefault().Value.UsingLanguage; } }
        #endregion

        #region communication
        public ContainerEventManager ContainerEventManager { get; set; }
        public ContainerOperationManager ContainerOperationManager { get; set; }
        internal ContainerResponseManager ContainerResponseManager { get; set; }
        #endregion

        public Container(int containerID, int entityID, string containerName)
        {
            ContainerID = containerID;
            EntityID = entityID;
            ContainerName = containerName;
            soulDictionary = new Dictionary<int, Soul>();
            ContainerEventManager = new ContainerEventManager(this);
            ContainerOperationManager = new ContainerOperationManager(this);
            ContainerResponseManager = new ContainerResponseManager(this);
        }
        public void BindEntity(Entity entity)
        {
            if(Entity == null)
            {
                Entity = entity;
            }
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
