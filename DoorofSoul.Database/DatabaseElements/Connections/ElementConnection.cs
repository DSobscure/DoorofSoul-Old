using DoorofSoul.Database.DatabaseElements.Connections.ElementConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class ElementConnection : DatabaseConnection
    {
        public abstract ItemsConnection ItemsConnection { get; }
    }
}
