using System;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.EntityElementsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.EntityElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLEntityElementsRepositoryList : EntityElementsRepositoryList
    {
        private MySQLEntitySpacePropertiesRepository entitySpacePropertiesRepository;
        public override EntitySpacePropertiesRepository EntitySpacePropertiesRepository { get { return entitySpacePropertiesRepository; } }

        public MySQLEntityElementsRepositoryList()
        {
            entitySpacePropertiesRepository = new MySQLEntitySpacePropertiesRepository();
        }
    }
}
