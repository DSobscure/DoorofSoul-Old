using DoorofSoul.Database.DatabaseElements.Connections.NatureConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class NatureConnection : DatabaseConnection
    {
        public ContainerElementsConnection ContainerElementsConnection { get; protected set; }
        public EntityElementsConnection EntityElementsConnection { get; protected set; }
    }
}
