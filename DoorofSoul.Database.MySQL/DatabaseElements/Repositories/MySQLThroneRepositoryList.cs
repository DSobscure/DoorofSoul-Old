using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLThroneRepositoryList : ThroneRepositoryList
    {
        private MySQLAnswerRepository answerRepository;

        public override AnswerRepository AnswerRepository { get { return answerRepository; } }

        public MySQLThroneRepositoryList()
        {
            answerRepository = new MySQLAnswerRepository();
        }
    }
}
