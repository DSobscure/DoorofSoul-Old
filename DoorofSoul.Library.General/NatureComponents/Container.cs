using DoorofSoul.Library.General.NatureComponents.ContainerElements;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers;
using DoorofSoul.Library.General.LightComponents.Effects;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DoorofSoul.Library.General.NatureComponents
{
    public class Container : IEffectorTarget
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
        public Inventory Inventory { get; protected set; }
        public ContainerAttributes Attributes { get; protected set; }
        public ContainerStatusEffectManager ContainerStatusEffectManager { get; protected set; }
        #endregion

        #region communication
        public ContainerEventManager ContainerEventManager { get; set; }
        public ContainerOperationManager ContainerOperationManager { get; set; }
        internal ContainerResponseManager ContainerResponseManager { get; set; }
        #endregion

        public Container(int containerID, int entityID, string containerName, ContainerAttributes attributes)
        {
            ContainerID = containerID;
            EntityID = entityID;
            ContainerName = containerName;
            Attributes = attributes;
            ContainerStatusEffectManager = new ContainerStatusEffectManager(this);
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
        public void BindInventory(Inventory inventory)
        {
            Inventory = inventory;
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
