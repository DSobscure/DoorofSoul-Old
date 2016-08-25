using System;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories;
using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.NatureRepositories
{
    class MySQLContainerElementsRepositoryList : ContainerElementsRepositoryList
    {
        private MySQLContainerAttributesRepository containerAttributesRepository;
        private MySQLContainerKernelAbilitiesRepository containerKernelAbilitiesRepository;
        private MySQLContainerKernelAbilitiesPotentialRepository containerKernelAbilitiesPotentialRepository;
        private MySQLInventoryRepository inventoryRepository;
        private MySQLInventoryItemInfoRepository mySQLInventoryItemInfoRepository;

        public override ContainerAttributesRepository ContainerAttributesRepository { get { return containerAttributesRepository; } }
        public override ContainerKernelAbilitiesRepository ContainerKernelAbilitiesRepository { get { return containerKernelAbilitiesRepository; } }
        public override ContainerKernelAbilitiesPotentialRepository ContainerKernelAbilitiesPotentialRepository { get { return containerKernelAbilitiesPotentialRepository; } }
        public override InventoryRepository InventoryRepository { get { return inventoryRepository; } }
        public override InventoryItemInfoRepository InventoryItemInfoRepository { get { return mySQLInventoryItemInfoRepository; } }

        public MySQLContainerElementsRepositoryList()
        {
            containerAttributesRepository = new MySQLContainerAttributesRepository();
            containerKernelAbilitiesRepository = new MySQLContainerKernelAbilitiesRepository();
            containerKernelAbilitiesPotentialRepository = new MySQLContainerKernelAbilitiesPotentialRepository();
            inventoryRepository = new MySQLInventoryRepository();
            mySQLInventoryItemInfoRepository = new MySQLInventoryItemInfoRepository();
        }
    }
}
