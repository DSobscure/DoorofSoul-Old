using DoorofSoul.Database.DatabaseElements;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements
{
    class MySQLRepositoryList : RepositoryList
    {
        private MySQLPlayerRepository playerRepository;

        private MySQLKnowledgeRepositoryList knowledgeRepositoryList;
        private MySQLElementRepositoryList elementRepositoryList;
        private MySQLLoveRepositoryList loveRepositoryList;
        private MySQLNatureRepositoryList natureRepositoryList;
        private MySQLThroneRepositoryList throneRepositoryList;

        public override PlayerRepository PlayerRepository { get { return playerRepository; } }

        public override KnowledgeRepositoryList KnowledgeRepositoryList { get { return knowledgeRepositoryList; } }
        public override ElementRepositoryList ElementRepositoryList { get { return elementRepositoryList; } }
        public override LoveRepositoryList LoveRepositoryList { get { return loveRepositoryList; } }
        public override NatureRepositoryList NatureRepositoryList { get { return natureRepositoryList; } }
        public override ThroneRepositoryList ThroneRepositoryList { get { return throneRepositoryList; } }

        public MySQLRepositoryList()
        {
            playerRepository = new MySQLPlayerRepository();

            knowledgeRepositoryList = new MySQLKnowledgeRepositoryList();
            elementRepositoryList = new MySQLElementRepositoryList();
            loveRepositoryList = new MySQLLoveRepositoryList();
            natureRepositoryList = new MySQLNatureRepositoryList();
            throneRepositoryList = new MySQLThroneRepositoryList();
        }
    }
}
