using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class ContainerRepository
    {
        public abstract Container Create(string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties);
        public abstract void Delete(int containerID);
        public abstract Container Find(int containerID);
        public abstract void Save(Container container);
        public abstract List<Container> List();
    }
}
