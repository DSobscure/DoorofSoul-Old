using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public abstract class ContainerKernelAbilitiesPotentialRepository
    {
        public abstract ContainerKernelAbilitiesPotential Create(int containerID, ContainerKernelAbilitiesPotential kernelAbilitiesPotential);
        public abstract void Delete(int containerID);
        public abstract ContainerKernelAbilitiesPotential Find(int containerID);
        public abstract void Save(int containerID, ContainerKernelAbilitiesPotential kernelAbilitiesPotential);
    }
}
