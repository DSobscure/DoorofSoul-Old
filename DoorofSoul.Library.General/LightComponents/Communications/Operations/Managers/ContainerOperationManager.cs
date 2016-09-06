using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers
{
    public class ContainerOperationManager
    {
        private readonly Dictionary<ContainerOperationCode, ContainerOperationHandler> operationTable;
        protected readonly Container container;
        public FetchDataResolver FetchDataResolver { get; protected set; }

        internal ContainerOperationManager(Container container)
        {
            this.container = container;
            FetchDataResolver = new FetchDataResolver(container);
            operationTable = new Dictionary<ContainerOperationCode, ContainerOperationHandler>
            {
                { ContainerOperationCode.FetchData, FetchDataResolver },
                { ContainerOperationCode.Say, new SayHandler(container) },
                { ContainerOperationCode.ObserveEntityTransform, new ObserveEntityTransformHandler(container) },
                { ContainerOperationCode.PickupItemEntity, new PickupItemEntityHandler(container) },
                { ContainerOperationCode.MoveInventoryItemInfo, new MoveInventoryItemInfoHandler(container) },
                { ContainerOperationCode.DiscardItem, new DiscardItemHandler(container) },
                { ContainerOperationCode.UseItem, new UseItemHandler(container) },
                { ContainerOperationCode.Rotate, new RotateHandler(container) },
                { ContainerOperationCode.Move, new MoveHandler(container) },
            };
        }

        internal void Operate(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryInstance.ErrorFormat("Container Operation Error: {0} from ContainerID: {1}", operationCode, container.ContainerID);
                }
            }
            else
            {
                LibraryInstance.ErrorFormat("Unknow Container Operation:{0} from ContainerID: {1}", operationCode, container.ContainerID);
            }
        }

        internal void SendOperation(ContainerOperationCode operationCode, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.OperationParameters.Answer.ContainerOperationParameterCode.Parameters, parameters }
                        };
                        if (!container.IsEmptyContainer)
                        {
                            container.FirstSoul.Answer.AnswerOperationManager.SendOperation(AnswerOperationCode.ContainerOperation, operationData);
                        }
                        else
                        {
                            LibraryInstance.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", container.ContainerID);
                        }
                    }
                    break;
                case ContainerCommunicationChannel.Scene:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.OperationParameters.Scene.ContainerOperationParameterCode.Parameters, parameters }
                        };
                        container.Entity.LocatedScene.SceneOperationManager.SendOperation(SceneOperationCode.ContainerOperation, operationData);
                    }
                    break;
                default:
                    LibraryInstance.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
        public void Rotate(int direction)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RotateParameterCode.Direction, direction }
            };
            SendOperation(ContainerOperationCode.Rotate, parameters, ContainerCommunicationChannel.Answer);
        }
        public void Move(int direction)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)MoveParameterCode.Direction, direction }
            };
            SendOperation(ContainerOperationCode.Move, parameters, ContainerCommunicationChannel.Answer);
        }
        public void Say(string message)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SayParameterCode.Message, message }
            };
            SendOperation(ContainerOperationCode.Say, parameters, ContainerCommunicationChannel.Answer);
        }
        public void ObserveEntityTransform(Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)ObserveEntityTransformParameterCode.EntityID, entity.EntityID },
                { (byte)ObserveEntityTransformParameterCode.Position, entity.Position },
                { (byte)ObserveEntityTransformParameterCode.Rotation, entity.Rotation }
            };
            SendOperation(ContainerOperationCode.ObserveEntityTransform, parameters, ContainerCommunicationChannel.Answer);
        }
        public void PickupItemEntity(int itemEntityID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)PickupItemEntityParameterCode.ItemEntityID, itemEntityID }
            };
            SendOperation(ContainerOperationCode.PickupItemEntity, parameters, ContainerCommunicationChannel.Answer);
        }
        public void MoveInventoryItemInfo(int originPosition, int newPosition)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)MoveInventoryItemInfoParameterCode.OriginPosition, newPosition },
                { (byte)MoveInventoryItemInfoParameterCode.NewPosition, originPosition }
            };
            SendOperation(ContainerOperationCode.MoveInventoryItemInfo, parameters, ContainerCommunicationChannel.Answer);
        }
        public void DiscardItem(int positionIndex)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)DiscardItemParameterCode.InventoryPositionIndex, positionIndex }
            };
            SendOperation(ContainerOperationCode.DiscardItem, parameters, ContainerCommunicationChannel.Answer);
        }
        public void UseItem(int positionIndex)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)UseItemParameterCode.InventoryPositionIndex, positionIndex }
            };
            SendOperation(ContainerOperationCode.UseItem, parameters, ContainerCommunicationChannel.Answer);
        }
    }
}
