using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public abstract class ContainerKernelAbilitiesRepository
    {
        public abstract ContainerKernelAbilities Create(int containerID, ContainerKernelAbilities kernelAbilities);
        public abstract void Delete(int containerID);
        public abstract ContainerKernelAbilities Find(int containerID);
        public abstract void Save(int containerID, ContainerKernelAbilities kernelAbilities);
    }
}
