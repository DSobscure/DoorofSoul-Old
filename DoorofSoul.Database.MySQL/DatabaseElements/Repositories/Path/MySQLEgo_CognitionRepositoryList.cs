using DoorofSoul.Database.DatabaseElements.Repositories.Path;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Database.DatabaseElements.Repositories.Path.Ego_CognitionRepositories;
using DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path.Ego_CognitionRepositories;

namespace DoorofSoul.Database.MySQL.DatabaseElements.Repositories.Path
{
    class MySQLEgo_CognitionRepositoryList : Ego_CognitionRepositoryList
    {
        private MySQLSoulContainerLinkRepository soulContainerLinkRepository;

        public override SoulContainerLinkRepository SoulContainerLinkRepository { get { return soulContainerLinkRepository; } }

        public MySQLEgo_CognitionRepositoryList()
        {
            soulContainerLinkRepository = new MySQLSoulContainerLinkRepository();
        }
    }
}
