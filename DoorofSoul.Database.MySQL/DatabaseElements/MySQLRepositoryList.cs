using DoorofSoul.Database.DatabaseElements;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements
{
    class MySQLRepositoryList : RepositoryList
    {
        protected override void InstantiateRepositories()
        {
            PlayerRepository = new MySQLPlayerRepository();

            KnowledgeRepositoryList = new MySQLKnowledgeRepositoryList();
            ElementRepositoryList = new MySQLElementRepositoryList();
            LoveRepositoryList = new MySQLLoveRepositoryList();
            NatureRepositoryList = new MySQLNatureRepositoryList();
            ThroneRepositoryList = new MySQLThroneRepositoryList();
        }
    }
}
