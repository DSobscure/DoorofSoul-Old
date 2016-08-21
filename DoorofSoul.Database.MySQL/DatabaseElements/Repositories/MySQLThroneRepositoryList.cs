using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.ThroneRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLThroneRepositoryList : ThroneRepositoryList
    {
        protected override void InstantiateRepositories()
        {
            AnswerRepository = new MySQLAnswerRepository();
            SoulRepository = new MySQLSoulRepository();
            SoulElementsRepositoryList = new MySQLSoulElementsRepositoryList();
        }
    }
}
