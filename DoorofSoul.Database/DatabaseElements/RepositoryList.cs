using DoorofSoul.Database.DatabaseElements.Repositories;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RepositoryList
    {
        public abstract PlayerRepository PlayerRepository {  get; }
        public abstract KnowledgeRepositoryList KnowledgeRepositoryList { get; }
        public abstract ElementRepositoryList ElementRepositoryList { get; }
        public abstract LightRepositoryList LightRepositoryList { get; }
        public abstract LoveRepositoryList LoveRepositoryList { get; }
        public abstract NatureRepositoryList NatureRepositoryList { get; }
        public abstract MindRepositoryList MindRepositoryList { get; }
        public abstract ThroneRepositoryList ThroneRepositoryList { get; }

        public abstract PathRepositoryList PathRepositoryList { get; }
    }
}
