using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories
{
    public abstract class ItemsRepositoryList : InstantiableRepositoryList
    {
        public ItemInfoRepository ItemInfoRepository { get; protected set; }
    }
}
