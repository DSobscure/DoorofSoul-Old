using DoorofSoul.Database.DatabaseElements.Connections.KnowledgeConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class KnowledgeConnection : DatabaseConnection
    {
        public abstract SkillsConnection SkillsConnection { get; }
        public abstract StatusEffectsConnection StatusEffectsConnection { get; }
    }
}
