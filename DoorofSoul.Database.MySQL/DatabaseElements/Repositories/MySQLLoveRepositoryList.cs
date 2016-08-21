using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LoveRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLLoveRepositoryList : LoveRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            SoulContainerLinkRepository = new MySQLSoulContainerLinkRepository();
        }
    }
}
