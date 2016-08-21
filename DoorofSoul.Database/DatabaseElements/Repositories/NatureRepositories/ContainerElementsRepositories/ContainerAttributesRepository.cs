using DoorofSoul.Library.General.NatureComponents.ContainerElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.ContainerElementsRepositories
{
    public abstract class ContainerAttributesRepository
    {
        public abstract ContainerAttributes Create(int containerID, ContainerAttributes containerAttributes);
        public abstract void Delete(int containerID);
        public abstract ContainerAttributes Find(int containerID);
        public abstract void Save(int containerID, ContainerAttributes containerAttributes);
    }
}
