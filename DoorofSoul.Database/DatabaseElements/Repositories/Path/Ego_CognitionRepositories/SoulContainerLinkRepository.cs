using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.Path.Ego_CognitionRepositories
{
    public abstract class SoulContainerLinkRepository
    {
        public abstract List<int> GetSoulIDs(int containerID);
        public abstract List<int> GetContainerIDs(int soulID);
        public abstract void Link_Soul_Container(int soulID, int containerID);
        public abstract void Unlink_Soul_Container(int soulID, int containerID);
    }
}
