using DoorofSoul.Database.DatabaseElements.Connections.NatureConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class NatureConnection : DatabaseConnection
    {
        public abstract ContainerElementsConnection ContainerElementsConnection { get; }
        public abstract EntityElementsConnection EntityElementsConnection { get; }
        public abstract SceneElementsConnection SceneElementsConnection { get; }
    }
}
