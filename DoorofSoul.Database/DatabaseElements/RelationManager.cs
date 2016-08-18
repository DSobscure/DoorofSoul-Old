using DoorofSoul.Database.DatabaseElements.Relations;

namespace DoorofSoul.Database.DatabaseElements
{
    public abstract class RelationManager
    {
        public SoulID_ContainerID_Relations SoulID_ContainerID_Relation { get; protected set; }

        public InventoryID_ItemInfo_Relations InventoryItemInfo_Relation { get; protected set; }
    }
}
