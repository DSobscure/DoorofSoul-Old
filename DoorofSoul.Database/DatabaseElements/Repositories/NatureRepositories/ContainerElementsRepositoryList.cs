using DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories
{
    public abstract class ContainerElementsRepositoryList : InstantiableRepositoryList
    {
        public ContainerAttributesRepository ContainerAttributesRepository { get; protected set; }
        public ContainerKernelAbilitiesRepository ContainerKernelAbilitiesRepository { get; protected set; }
        public ContainerKernelAbilitiesPotentialRepository ContainerKernelAbilitiesPotentialRepository { get; protected set; }
        public InventoryRepository InventoryRepository { get; protected set; }
    }
}
