using DoorofSoul.Database.DatabaseElements.Relations;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RelationManager
    {
        public SoulID_ContainerID_Relation SoulID_ContainerID_Relation { get; protected set; }

        public InventoryItemInfo_Relation InventoryItemInfo_Relation { get; protected set; }
    }
}
