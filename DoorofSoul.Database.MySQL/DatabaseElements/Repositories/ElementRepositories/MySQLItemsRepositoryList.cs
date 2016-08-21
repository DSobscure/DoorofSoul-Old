using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories
{
    class MySQLItemsRepositoryList : ItemsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            ItemInfoRepository = new MySQLItemInfoRepository();
        }
    }
}
