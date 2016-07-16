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
        public string ContainerName { get; set; }
        protected Dictionary<int, Soul> soulDictionary;

        public Container(int containerID, string containerName, int entityID, int locatedSceneID, EntitySpaceProperties spaceProperties) : base(entityID, locatedSceneID, spaceProperties)
        {
            soulDictionary = new Dictionary<int, Soul>();
        }
    }
}
