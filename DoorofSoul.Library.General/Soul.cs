﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Soul
    {
        public int SoulID { get; protected set; }
        public int AnswerID { get; protected set; }
        public Answer Answer { get; protected set; }
        public string SoulName { get; set; }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        public int ContainerCount { get { return containerDictionary.Count; } }
        public bool IsActive { get; set; }

        public Soul(int soulID, Answer answer, string soulName)
        {
            SoulID = soulID;
            AnswerID = answer.AnswerID;
            Answer = answer;
            SoulName = soulName;
            containerDictionary = new Dictionary<int, Container>();
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
