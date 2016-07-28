using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Entity;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class EntityOperationManager
    {
        protected readonly Dictionary<EntityOperationCode, EntityOperationHandler> operationTable;
        protected readonly Entity entity;

        public EntityOperationManager(Entity entity)
        {
            this.entity = entity;
            operationTable = new Dictionary<EntityOperationCode, EntityOperationHandler>
            {
                { EntityOperationCode.FetchData, new FetchDataResolver(entity) },
            };
        }

        public void Operate(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Entity Operation Error: {0} from EntityID: {1}", operationCode, entity.EntityID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Entity Operation:{0} from EntityID: {1}", operationCode, entity.EntityID);
            }
        }
    }
}
