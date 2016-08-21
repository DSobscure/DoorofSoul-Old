using DoorofSoul.Library.General.ThroneComponents.SoulElements;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories
{
    public abstract class SoulKernelAbilitiesRepository
    {
        public abstract SoulKernelAbilities Create(int soulID, SoulKernelAbilities kernelAbilities);
        public abstract void Delete(int soulID);
        public abstract SoulKernelAbilities Find(int soulID);
        public abstract void Save(int soulID, SoulKernelAbilities kernelAbilities);
    }
}
