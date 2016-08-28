using DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories.SoulElementsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.MindRepositories
{
    public abstract class SoulElementsRepositoryList
    {
        public abstract SoulAttributesRepository SoulAttributesRepository { get; }
        public abstract SoulKernelAbilitiesRepository SoulKernelAbilitiesRepository { get; }
        public abstract SoulPhaseRepository SoulPhaseRepository { get; }
    }
}
