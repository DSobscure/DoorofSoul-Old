using DoorofSoul.Database.DatabaseElements.Connections;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class ConnectionList : DatabaseConnection
    {
        public KnowledgeConnection KnowledgeConnection { get; protected set; }
        public ElementConnection ElementConnection { get; protected set; }
        public LoveConnection LoveConnection { get; protected set; }
        public NatureConnection NatureConnection { get; protected set; }
        public ThroneConnection ThroneConnection { get; protected set; }
    }
}
