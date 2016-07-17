using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorofSoul.Server.DatabaseElements.Relations
{
    public abstract class SoulID_ContainerID_Relation
    {
        public abstract List<int> GetSoulIDs(int containerID);
        public abstract List<int> GetContainerIDs(int soulID);
        public abstract void Link_Soul_Container(int soulID, int containerID);
        public abstract void Unlink_Soul_Container(int soulID, int containerID);
    }
}
