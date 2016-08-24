using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLNatureRepositoryList : NatureRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            WorldRepository = new MySQLWorldRepository();
            SceneRepository = new MySQLSceneRepository();
            ContainerRepository = new MySQLContainerRepository();
            EntityRepository = new MySQLEntityRepository();

            ContainerElementsRepositoryList = new MySQLContainerElementsRepositoryList();
            EntityElementsRepositoryList = new MySQLEntityElementsRepositoryList();
            SceneElementsRepositoryList = new MySQLSceneElementsRepositoryList();
        }
    }
}
