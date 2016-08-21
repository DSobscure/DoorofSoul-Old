using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ElementRepositoryList : InstantiableRepositoryList
    {
        public ItemRepository ItemRepository { get; protected set; }
        public ItemsRepositoryList ItemsRepositoryList { get; protected set; }
    }
}
