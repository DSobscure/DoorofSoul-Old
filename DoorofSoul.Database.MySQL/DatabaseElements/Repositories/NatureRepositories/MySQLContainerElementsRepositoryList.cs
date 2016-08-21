using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLContainerElementsRepositoryList : ContainerElementsRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            ContainerAttributesRepository = new MySQLContainerAttributesRepository();
            ContainerKernelAbilitiesRepository = new MySQLContainerKernelAbilitiesRepository();
            ContainerKernelAbilitiesPotentialRepository = new MySQLContainerKernelAbilitiesPotentialRepository();
            InventoryRepository = new MySQLInventoryRepository();
        }
    }
}
