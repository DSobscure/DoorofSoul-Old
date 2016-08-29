using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories.ItemsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories
{
    class MySQLItemsRepositoryList : ItemsRepositoryList
    {
        private MySQLConsumablesRepository consumablesRepository;
        public override ConsumablesRepository ConsumablesRepository { get { return consumablesRepository; } }

        public MySQLItemsRepositoryList()
        {
            consumablesRepository = new MySQLConsumablesRepository();
        }
    }
}
