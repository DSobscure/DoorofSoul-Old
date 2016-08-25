using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories
{
    public abstract class ContainerElementsRepositoryList
    {
        public abstract ContainerAttributesRepository ContainerAttributesRepository { get; }
        public abstract ContainerKernelAbilitiesRepository ContainerKernelAbilitiesRepository { get; }
        public abstract ContainerKernelAbilitiesPotentialRepository ContainerKernelAbilitiesPotentialRepository { get; }
        public abstract InventoryRepository InventoryRepository { get; }
        public abstract InventoryItemInfoRepository InventoryItemInfoRepository { get; }
    }
}
