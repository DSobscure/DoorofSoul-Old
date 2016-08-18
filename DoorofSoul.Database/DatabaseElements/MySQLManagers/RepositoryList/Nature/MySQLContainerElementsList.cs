using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Nature.ContainerElements;
using DoorofSoul.Database.DatabaseElements.RepositoryList.Nature;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList.Nature
{
    public class MySQLContainerElementsList : ContainerElementsList
    {
        public MySQLContainerElementsList()
        {
            InventoryRepository = new MySQLInventoryRepository();
        }
    }
}
