using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class KnowledgeRepositoryList
    {
        public abstract SkillRepository SkillRepository { get; }
        public abstract StatusEffectRepository StatusEffectRepository { get; }
        public abstract SkillsRepositoryList SkillsRepositoryList { get; }
        public abstract StatusEffectsRepositoryList StatusEffectsRepositoryList { get; }
    }
}
