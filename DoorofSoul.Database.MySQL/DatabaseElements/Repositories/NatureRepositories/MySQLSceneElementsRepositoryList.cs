using System;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLSceneElementsRepositoryList : SceneElementsRepositoryList
    {
        private MySQLItemEntityRepository itemEntityRepository;

        public override ItemEntityRepository ItemEntityRepository { get { return itemEntityRepository; } }

        public MySQLSceneElementsRepositoryList()
        {
            itemEntityRepository = new MySQLItemEntityRepository();
        }
    }
}
