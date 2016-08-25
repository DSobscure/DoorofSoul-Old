using System;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.ElementRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ElementRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLElementRepositoryList : ElementRepositoryList
    {
        private MySQLItemRepository itemRepository;

        private MySQLItemsRepositoryList itemsRepositoryList;

        public override ItemRepository ItemRepository { get { return itemRepository; } }
        public override ItemsRepositoryList ItemsRepositoryList { get { return itemsRepositoryList; } }

        public MySQLElementRepositoryList()
        {
            itemRepository = new MySQLItemRepository();

            itemsRepositoryList = new MySQLItemsRepositoryList();
        }
    }
}
