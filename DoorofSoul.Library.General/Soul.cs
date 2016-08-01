using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
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
        #endregion

        #region events
        private event Action<Soul> onSoulActivate;
        public event Action<Soul> OnSoulActivate
        {
            add { onSoulActivate += value; }
            remove { onSoulActivate -= value; }
        }
        #endregion

        #region communication
        public SoulEventManager SoulEventManager { get; set; }
        public SoulOperationManager SoulOperationManager { get; set; }
        internal SoulResponseManager SoulResponseManager { get; set; }
        #endregion

        public Soul(int soulID, Answer answer, string soulName)
        {
            SoulID = soulID;
            AnswerID = answer.AnswerID;
            Answer = answer;
            SoulName = soulName;
            containerDictionary = new Dictionary<int, Container>();
            SoulEventManager = new SoulEventManager(this);
            SoulOperationManager = new SoulOperationManager(this);
            SoulResponseManager = new SoulResponseManager(this);
        }
        public void LinkContainer(Container container)
        {
            if(!containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
            }
        }
        public void UnlinkContainer(Container container)
        {
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Remove(container.ContainerID);
            }
        }
        public void UnlinkAllContainers()
        {
            containerDictionary.Clear();
        }
    }
}
