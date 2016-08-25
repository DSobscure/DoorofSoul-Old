using System;
using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.Repositories.LoveRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.LoveRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories
{
    class MySQLLoveRepositoryList : LoveRepositoryList
    {
        private MySQLSoulContainerLinkRepository soulContainerLinkRepository;

        public override SoulContainerLinkRepository SoulContainerLinkRepository { get { return soulContainerLinkRepository; } }

        public MySQLLoveRepositoryList()
        {
            soulContainerLinkRepository = new MySQLSoulContainerLinkRepository();
        }
    }
}
