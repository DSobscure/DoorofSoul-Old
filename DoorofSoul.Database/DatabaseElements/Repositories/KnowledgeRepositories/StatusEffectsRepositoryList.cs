using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories
{
    public abstract class StatusEffectsRepositoryList : InstantiableRepositoryList
    {
        public ContainerStatusEffectInfoRepository ContainerStatusEffectInfoRepository { get; protected set; }
    }
}
