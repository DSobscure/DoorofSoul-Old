using DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList.Nature;
using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature;
using DoorofSoul.Database.DatabaseElements.RepositoryList;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList
{
    public class MySQLNatureList : NatureList
    {
        public MySQLNatureList()
        {
            ContainerRepository = new MySQLContainerRepository();
            EntityRepository = new MySQLEntityRepository();
            WorldRepository = new MySQLWorldRepository();
            SceneRepository = new MySQLSceneRepository();

            ContainerElementsList = new MySQLContainerElementsList();
            EntityElementsList = new MySQLEntityElementsList();
        }
    }
}
