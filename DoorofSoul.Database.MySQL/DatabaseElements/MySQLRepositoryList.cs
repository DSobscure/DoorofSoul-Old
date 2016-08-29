using System;
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
        private MySQLLightRepositoryList lightRepositoryList;
        private MySQLLoveRepositoryList loveRepositoryList;
        private MySQLNatureRepositoryList natureRepositoryList;
        private MySQLMindRepositoryList mindRepositoryList;
        private MySQLThroneRepositoryList throneRepositoryList;

        private MySQLPathRepositoryList pathRepositoryList;

        public override PlayerRepository PlayerRepository { get { return playerRepository; } }

        public override KnowledgeRepositoryList KnowledgeRepositoryList { get { return knowledgeRepositoryList; } }
        public override ElementRepositoryList ElementRepositoryList { get { return elementRepositoryList; } }
        public override LightRepositoryList LightRepositoryList { get { return lightRepositoryList; } }
        public override LoveRepositoryList LoveRepositoryList { get { return loveRepositoryList; } }
        public override NatureRepositoryList NatureRepositoryList { get { return natureRepositoryList; } }
        public override MindRepositoryList MindRepositoryList { get { return mindRepositoryList; } }
        public override ThroneRepositoryList ThroneRepositoryList { get { return throneRepositoryList; } }

        public override PathRepositoryList PathRepositoryList { get { return pathRepositoryList; } }

        public MySQLRepositoryList()
        {
            playerRepository = new MySQLPlayerRepository();

            knowledgeRepositoryList = new MySQLKnowledgeRepositoryList();
            elementRepositoryList = new MySQLElementRepositoryList();
            lightRepositoryList = new MySQLLightRepositoryList();
            loveRepositoryList = new MySQLLoveRepositoryList();
            natureRepositoryList = new MySQLNatureRepositoryList();
            mindRepositoryList = new MySQLMindRepositoryList();
            throneRepositoryList = new MySQLThroneRepositoryList();

            pathRepositoryList = new MySQLPathRepositoryList();
        }
    }
}
