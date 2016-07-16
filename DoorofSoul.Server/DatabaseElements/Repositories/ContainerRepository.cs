using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class ContainerRepository
    {
        public abstract Container Find(int containerID);
        public abstract void Save(Container container);
        public abstract List<Container> List();
        public abstract List<Container> ListOfSoul(int soulID);
    }
}
