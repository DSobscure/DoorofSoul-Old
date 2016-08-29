using DoorofSoul.Database.DatabaseElements.Connections.Path;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class PathConnectionList : DatabaseConnection
    {
        public abstract Ego_CognitionConnection Ego_CognitionConnection { get; }
    }
}
