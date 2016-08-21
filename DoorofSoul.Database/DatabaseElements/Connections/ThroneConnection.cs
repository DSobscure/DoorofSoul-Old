using DoorofSoul.Database.DatabaseElements.Connections.ThroneConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class ThroneConnection : DatabaseConnection
    {
        public SoulElementsConnection SoulElementsConnection { get; protected set; }
    }
}
