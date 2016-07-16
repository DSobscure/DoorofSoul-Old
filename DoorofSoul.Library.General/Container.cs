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

        public Container(int containerID, int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties) : base(entityID, entityName, locatedSceneID, spaceProperties)
        {
            ContainerID = containerID;
            soulDictionary = new Dictionary<int, Soul>();
        }
    }
}
