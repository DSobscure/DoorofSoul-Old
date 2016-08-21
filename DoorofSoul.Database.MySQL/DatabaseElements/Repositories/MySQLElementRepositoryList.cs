using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLElementRepositoryList : ElementRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            ItemRepository = new MySQLItemRepository();

            ItemsRepositoryList = new MySQLItemsRepositoryList();
        }
    }
}
