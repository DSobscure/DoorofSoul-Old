using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories
{
    public abstract class SoulElementsRepositoryList : InstantiableRepositoryList
    {
        public SoulAttributesRepository SoulAttributesRepository { get; protected set; }
        public SoulKernelAbilitiesRepository SoulKernelAbilitiesRepository { get; protected set; }
        public SoulPhaseRepository SoulPhaseRepository { get; protected set; }
    }
}
