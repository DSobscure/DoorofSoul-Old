using DoorofSoul.Database.DatabaseElements.Connections.LightConnections;

namespace DoorofSoul.Database.DatabaseElements.Connections
{
    public abstract class LightConnection : DatabaseConnection
    {
        public abstract EffectsConnection EffectsConnection { get; }
    }
}
