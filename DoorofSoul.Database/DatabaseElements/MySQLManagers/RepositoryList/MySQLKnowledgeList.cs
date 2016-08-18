using DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList.Knowledge;
using DoorofSoul.Database.DatabaseElements.Repositories.MySQL.Knowledge;
using DoorofSoul.Database.DatabaseElements.RepositoryList;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers.RepositoryList
{
    public class MySQLKnowledgeList : KnowledgeList
    {
        public MySQLKnowledgeList()
        {
            ItemRepository = new MySQLItemRepository();

            SkillsList = new MySQLSkillsList();
        }
    }
}
