using System;
using DoorofSoul.Database.DatabaseElements.Relations.MySQL;

namespace DoorofSoul.Database.DatabaseElements.MySQLManagers
{
    class MySQLRelationManager : RelationManager
    {
        public MySQLRelationManager()
        {
            SoulID_ContainerID_Relation = new MySQL_SoulID_ContainerID_Relation();
            InventoryItemInfo_Relation = new MySQL_InventoryItemInfo_Relation();
        }
    }
}
