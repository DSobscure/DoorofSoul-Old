using System;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.ThroneRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLThroneRepositoryList : ThroneRepositoryList
    {
        private MySQLAnswerRepository answerRepository;
        private MySQLSoulRepository soulRepository;

        private MySQLSoulElementsRepositoryList soulElementsRepositoryList;

        public override AnswerRepository AnswerRepository { get { return answerRepository; } }

        public override SoulRepository SoulRepository { get { return soulRepository; } }
        public override SoulElementsRepositoryList SoulElementsRepositoryList { get { return soulElementsRepositoryList; } }

        public MySQLThroneRepositoryList()
        {
            answerRepository = new MySQLAnswerRepository();
            soulRepository = new MySQLSoulRepository();

            soulElementsRepositoryList = new MySQLSoulElementsRepositoryList();
        }
    }
}
