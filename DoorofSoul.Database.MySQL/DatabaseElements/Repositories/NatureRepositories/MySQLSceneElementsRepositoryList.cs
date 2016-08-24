using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLSceneElementsRepositoryList : SceneElementsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            ItemEntityRepository = new MySQLItemEntityRepository();
        }
    }
}
