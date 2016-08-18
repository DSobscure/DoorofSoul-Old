using DoorofSoul.Database.DatabaseElements.Repositories;
using DoorofSoul.Database.DatabaseElements.RepositoryList;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RepositoryManager
    {
        public PlayerRepository PlayerRepository { get; protected set; }
        public KnowledgeList KnowledgeList { get; protected set; }
        public NatureList NatureList { get; protected set; }
        public ThroneList ThroneList { get; protected set; }
    }
}
