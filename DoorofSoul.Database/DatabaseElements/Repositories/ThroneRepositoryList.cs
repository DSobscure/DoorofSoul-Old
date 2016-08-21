using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ThroneRepositoryList : InstantiableRepositoryList
    {
        public AnswerRepository AnswerRepository { get; protected set; }
        public SoulRepository SoulRepository { get; protected set; }
        public SoulElementsRepositoryList SoulElementsRepositoryList { get; protected set; }
    }
}
