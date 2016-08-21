using DoorofSoul.Database.DatabaseElements.Repositories.LoveRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class LoveRepositoryList : InstantiableRepositoryList
    {
        public SoulContainerLinkRepository SoulContainerLinkRepository { get; protected set; }
    }
}
