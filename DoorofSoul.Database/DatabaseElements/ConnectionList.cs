using DoorofSoul.Database.DatabaseElements.Connections;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class ConnectionList : DatabaseConnection
    {
        public abstract KnowledgeConnection KnowledgeConnection { get; }
        public abstract ElementConnection ElementConnection { get; }
        public abstract LoveConnection LoveConnection { get; }
        public abstract NatureConnection NatureConnection { get; }
        public abstract MindConnection MindConnection{ get; }
        public abstract ThroneConnection ThroneConnection { get; }
    }
}
