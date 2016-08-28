using DoorofSoul.Database.DatabaseElements.Connections.MindConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class MindConnection : DatabaseConnection
    {
        public abstract SoulElementsConnection SoulElementsConnection { get; }
    }
}
