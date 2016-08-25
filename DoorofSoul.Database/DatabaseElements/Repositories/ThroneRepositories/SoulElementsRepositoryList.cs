using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories
{
    public abstract class SoulElementsRepositoryList
    {
        public abstract SoulAttributesRepository SoulAttributesRepository { get; }
        public abstract SoulKernelAbilitiesRepository SoulKernelAbilitiesRepository { get; }
        public abstract SoulPhaseRepository SoulPhaseRepository { get; }
    }
}
