using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;

namespace DoorofSoul.Database.DatabaseElements.Repositories
{
    public abstract class ThroneRepositoryList
    {
        public abstract AnswerRepository AnswerRepository { get; }
        public abstract SoulRepository SoulRepository { get; }
        public abstract SoulElementsRepositoryList SoulElementsRepositoryList { get; }
    }
}
