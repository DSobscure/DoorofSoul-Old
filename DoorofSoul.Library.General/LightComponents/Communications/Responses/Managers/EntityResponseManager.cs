using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Entity;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Scene;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers
{
    internal class EntityResponseManager
    {
        protected readonly Dictionary<EntityOperationCode, EntityResponseHandler> operationTable;
        protected readonly Entity entity;

        internal EntityResponseManager(Entity entity)
        {
            this.entity = entity;
            operationTable = new Dictionary<EntityOperationCode, EntityResponseHandler>
            {
                { EntityOperationCode.FetchData, new FetchDataResponseResolver(entity) }
            };
        }

        internal void Operate(EntityOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryInstance.ErrorFormat("Entity Response Error: {0} from AnswerID: {1}", operationCode, entity.EntityID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Entity Response:{0} from AnswerID: {1}", operationCode, entity.EntityID);
            }
        }

        internal void SendResponse(EntityOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationData = new Dictionary<byte, object>
            {
                { (byte)EntityResponseParameterCode.EntityID, entity.EntityID },
                { (byte)EntityResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)EntityResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)EntityResponseParameterCode.DebugMessage, debugMessage },
                { (byte)EntityResponseParameterCode.Parameters, parameters }
            };
            entity.LocatedScene.SceneResponseManager.SendResponse(SceneOperationCode.EntityOperation, ErrorCode.NoError, null, operationData);
        }
    }
}
