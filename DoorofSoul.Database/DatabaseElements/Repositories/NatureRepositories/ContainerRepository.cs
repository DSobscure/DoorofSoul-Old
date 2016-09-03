﻿using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories
{
    public abstract class ContainerRepository
    {
        public abstract Container Create(string containerName, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties);
        public abstract void Delete(int containerID);
        public abstract Container Find(int containerID);
        public abstract void Save(Container container);
        public abstract List<Container> List();
    }
}
