using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class MindRepositoryList
    {
        public abstract SoulRepository SoulRepository { get; }
        public abstract SoulElementsRepositoryList SoulElementsRepositoryList { get; }
    }
}
