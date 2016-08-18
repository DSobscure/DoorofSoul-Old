using DoorofSoul.Library.General.LightComponents.Communications.Events.Managers;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers;
using DoorofSoul.Library.General.ThroneComponents.SoulElements;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.ThroneComponents
{
    public class Soul
    {
        #region properties
        public int SoulID { get; protected set; }
        public int AnswerID { get; protected set; }
        public Answer Answer { get; protected set; }
        public string SoulName { get; set; }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        public int ContainerCount { get { return containerDictionary.Count; } }
        private bool isActivate;
        public bool IsActivate
        {
            get { return isActivate; }
            set
            {
                isActivate = value;
                onSoulActivate?.Invoke(this);
            }
        }
        public SupportLauguages UsingLanguage { get { return Answer.UsingLanguage; } }
        public SoulAttributes Attributes { get; protected set; }
        public SkillLibrary SkillLibrary { get; protected set; }
        public SkillKnowledgeInterface SkillKnowledgeInterface { get; protected set; }
    #endregion

        #region events
        private event Action<Soul> onSoulActivate;
        public event Action<Soul> OnSoulActivate { add { onSoulActivate += value; } remove { onSoulActivate -= value; } }
        #endregion

        #region communication
        public SoulEventManager SoulEventManager { get; set; }
        public SoulOperationManager SoulOperationManager { get; set; }
        internal SoulResponseManager SoulResponseManager { get; set; }
        #endregion

        public Soul(int soulID, Answer answer, string soulName, SoulAttributes attributes)
        {
            SoulID = soulID;
            AnswerID = answer.AnswerID;
            Answer = answer;
            SoulName = soulName;
            Attributes = attributes;
            
            containerDictionary = new Dictionary<int, Container>();
            SoulEventManager = new SoulEventManager(this);
            SoulOperationManager = new SoulOperationManager(this);
            SoulResponseManager = new SoulResponseManager(this);
            SkillLibrary = new SkillLibrary(this);
        }
        public void LinkContainer(Container container)
        {
            if(!ContainsContainer(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
            }
        }
        public void UnlinkContainer(Container container)
        {
            if (ContainsContainer(container.ContainerID))
            {
                containerDictionary.Remove(container.ContainerID);
            }
        }
        public void UnlinkAllContainers()
        {
            containerDictionary.Clear();
        }
        public void BindSkillKnowledgeInterface(SkillKnowledgeInterface skillKnowledgeInterface)
        {
            SkillKnowledgeInterface = skillKnowledgeInterface;
        }

        public bool ContainsContainer(int containerID)
        {
            return containerDictionary.ContainsKey(containerID);
        }
        public Container FindContainer(int containerID)
        {
            if(ContainsContainer(containerID))
            {
                return containerDictionary[containerID];
            }
            else
            {
                return null;
            }
        }
    }
}
