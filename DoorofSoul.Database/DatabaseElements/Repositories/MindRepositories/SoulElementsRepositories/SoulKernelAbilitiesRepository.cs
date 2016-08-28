using DoorofSoul.Library.General.MindComponents.SoulElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories
{
    public abstract class SoulKernelAbilitiesRepository
    {
        public abstract SoulKernelAbilities Create(int soulID, SoulKernelAbilities kernelAbilities);
        public abstract void Delete(int soulID);
        public abstract SoulKernelAbilities Find(int soulID);
        public abstract void Save(int soulID, SoulKernelAbilities kernelAbilities);
    }
}
