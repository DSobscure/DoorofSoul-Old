using DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.SkillsRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories
{
    public abstract class SkillsRepositoryList : InstantiableRepositoryList
    {
        public SkillInfoRepository SkillInfoRepository { get; protected set; }
    }
}
