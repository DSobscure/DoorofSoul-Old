using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Entity;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Managers
{
    public class EntityResponseManager
    {
        protected readonly Dictionary<EntityOperationCode, EntityResponseHandler> operationTable;
        protected readonly Entity entity;

        public EntityResponseManager(Entity entity)
        {
            this.entity = entity;
            operationTable = new Dictionary<EntityOperationCode, EntityResponseHandler>
            {
                { EntityOperationCode.FetchData, new FetchDataResponseResolver(entity) }
            };
        }

        public void Operate(EntityOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Entity Response Error: {0} from AnswerID: {1}", operationCode, entity.EntityID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Entity Response:{0} from AnswerID: {1}", operationCode, entity.EntityID);
            }
        }
    }
}
