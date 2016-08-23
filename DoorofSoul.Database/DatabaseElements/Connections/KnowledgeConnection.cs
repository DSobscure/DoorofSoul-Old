using DoorofSoul.Database.DatabaseElements.Connections.KnowledgeConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class KnowledgeConnection : DatabaseConnection
    {
        public SkillsConnection SkillsConnection { get; protected set; }
        public StatusEffectsConnection StatusEffectsConnection { get; protected set; }
    }
}
