﻿using DoorofSoul.Library.General.NatureComponents;
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
                { ContainerOperationCode.ObserveEntityPosition, new ObserveEntityPositionHandler(container) },
                { ContainerOperationCode.PickupItemEntity, new PickupItemEntityHandler(container) },
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
        public void Say(string message)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SayParameterCode.Message, message }
            };
            SendOperation(ContainerOperationCode.Say, parameters, ContainerCommunicationChannel.Answer);
        }
        public void ObserveEntityPosition(Entity entity)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)ObserveEntityPositionParameterCode.EntityID, entity.EntityID },
                { (byte)ObserveEntityPositionParameterCode.Position, entity.Position }
            };
            SendOperation(ContainerOperationCode.ObserveEntityPosition, parameters, ContainerCommunicationChannel.Answer);
        }
        public void PickupItemEntity(int itemEntityID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)PickupItemEntityParameterCode.ItemEntityID, itemEntityID }
            };
            SendOperation(ContainerOperationCode.PickupItemEntity, parameters, ContainerCommunicationChannel.Answer);
        }
    }
}