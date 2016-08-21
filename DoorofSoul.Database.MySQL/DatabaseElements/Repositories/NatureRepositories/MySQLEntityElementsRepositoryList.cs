using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.EntityElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLEntityElementsRepositoryList : EntityElementsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            EntitySpacePropertiesRepository = new MySQLEntitySpacePropertiesRepository();
        }
    }
}
