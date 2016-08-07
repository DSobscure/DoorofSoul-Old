using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Entity;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Scene;
using DoorofSoul.Protocol.Communication.OperationParameters.Entity;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class EntityOperationManager
    {
        private readonly Dictionary<EntityOperationCode, EntityOperationHandler> operationTable;
        protected readonly Entity entity;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        internal EntityOperationManager(Entity entity)
        {
            this.entity = entity;
            FetchDataResolver = new FetchDataResolver(entity);
            operationTable = new Dictionary<EntityOperationCode, EntityOperationHandler>
            {
                { EntityOperationCode.FetchData, FetchDataResolver },
                { EntityOperationCode.Rotate, new RotateHandler(entity) },
                { EntityOperationCode.Move, new MoveHandler(entity) },
            };
        }

        internal void Operate(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Entity Operation Error: {0} from EntityID: {1}", operationCode, entity.EntityID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Entity Operation:{0} from EntityID: {1}", operationCode, entity.EntityID);
            }
        }

        internal void SendOperation(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)EntityOperationParameterCode.EntityID, entity.EntityID },
                { (byte)EntityOperationParameterCode.OperationCode, (byte)operationCode },
                { (byte)EntityOperationParameterCode.Parameters, parameters }
            };
            entity.LocatedScene.SceneOperationManager.SendOperation(SceneOperationCode.EntityOperation, eventData);
        }

        public void Rotate(int direction)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RotateParameterCode.Direction, direction }
            };
            SendOperation(EntityOperationCode.Rotate, parameters);
        }
        public void Move(int direction)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)MoveParameterCode.Direction, direction }
            };
            SendOperation(EntityOperationCode.Move, parameters);
        }
    }
}
