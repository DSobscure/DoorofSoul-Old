using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ElementRepositoryList
    {
        public abstract ItemRepository ItemRepository { get; }
        public abstract ItemsRepositoryList ItemsRepositoryList { get; }
    }
}
