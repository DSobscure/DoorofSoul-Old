using DoorofSoul.Database.DatabaseElements.Repositories;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RepositoryList : InstantiableRepositoryList
    {
        public PlayerRepository PlayerRepository { get; protected set; }
        public KnowledgeRepositoryList KnowledgeRepositoryList { get; protected set; }
        public ElementRepositoryList ElementRepositoryList { get; protected set; }
        public LoveRepositoryList LoveRepositoryList { get; protected set; }
        public NatureRepositoryList NatureRepositoryList { get; protected set; }
        public ThroneRepositoryList ThroneRepositoryList { get; protected set; }
    }
}
