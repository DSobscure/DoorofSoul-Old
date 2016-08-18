using DoorofSoul.Database.DatabaseElements.Repositories.Knowledge;
using DoorofSoul.Database.DatabaseElements.RepositoryList.Knowledge;

namespace DoorofSoul.Database.DatabaseElements.RepositoryList
{
    public class KnowledgeList
    {
        public ItemRepository ItemRepository { get; protected set; }

        public SkillsList SkillsList { get; protected set; }
    }
}
