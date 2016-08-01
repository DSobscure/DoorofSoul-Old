using DoorofSoul.Library.General.Operations.Handlers;
using DoorofSoul.Library.General.Operations.Handlers.Container;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Managers
{
    public class ContainerOperationManager
    {
        private readonly Dictionary<ContainerOperationCode, ContainerOperationHandler> operationTable;
        protected readonly Container container;

        internal ContainerOperationManager(Container container)
        {
            this.container = container;
            operationTable = new Dictionary<ContainerOperationCode, ContainerOperationHandler>
            {
                { ContainerOperationCode.FetchData, new FetchDataResolver(container) },
                { ContainerOperationCode.Say, new SayHandler(container) }
            };
        }

        internal void Operate(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LibraryLog.ErrorFormat("Container Operation Error: {0} from ContainerID: {1}", operationCode, container.ContainerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Container Operation:{0} from ContainerID: {1}", operationCode, container.ContainerID);
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
                            LibraryLog.ErrorFormat("Not Exist Soul for Container Communication, ContainerID: {0}", container.ContainerID);
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
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }

        public void FetchEntity()
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)ContainerFetchDataCode.Entity },
                { (byte)FetchDataParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            SendOperation(ContainerOperationCode.FetchData, fetchDataParameters, ContainerCommunicationChannel.Answer);
        }
        public void Say(string message)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)SayParameterCode.Message, message }
            };
            SendOperation(ContainerOperationCode.Say, parameters, ContainerCommunicationChannel.Answer);
        }
    }
}
