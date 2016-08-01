using DoorofSoul.Library.General.Responses.Handlers;
using DoorofSoul.Library.General.Responses.Handlers.Container;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication.Channels;

namespace DoorofSoul.Library.General.Responses.Managers
{
    internal class ContainerResponseManager
    {
        protected readonly Dictionary<ContainerOperationCode, ContainerResponseHandler> operationTable;
        protected readonly Container container;

        internal ContainerResponseManager(Container container)
        {
            this.container = container;
            operationTable = new Dictionary<ContainerOperationCode, ContainerResponseHandler>
            {
                { ContainerOperationCode.FetchData, new FetchDataResponseResolver(container) }
            };
        }

        internal void Operate(ContainerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LibraryLog.ErrorFormat("Container Response Error: {0} from AnswerID: {1}", operationCode, container.ContainerID);
                }
            }
            else
            {
                LibraryLog.ErrorFormat("Unknow Container Response:{0} from AnswerID: {1}", operationCode, container.ContainerID);
            }
        }

        internal void SendResponse(ContainerOperationCode operationCode, ErrorCode errorCode, string debugMessage, Dictionary<byte, object> parameters, ContainerCommunicationChannel channel)
        {
            switch (channel)
            {
                case ContainerCommunicationChannel.Answer:
                    {
                        Dictionary<byte, object> operationData = new Dictionary<byte, object>
                        {
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.ReturnCode, (short)errorCode },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.DebugMessage, debugMessage },
                            { (byte)Protocol.Communication.ResponseParameters.Answer.ContainerResponseParameterCode.Parameters, parameters }
                        };
                        if (!container.IsEmptyContainer)
                        {
                            container.FirstSoul.Answer.AnswerResponseManager.SendResponse(AnswerOperationCode.ContainerOperation, ErrorCode.NoError, null, operationData);
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
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.ContainerID, container.ContainerID },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.OperationCode, (byte)operationCode },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.ReturnCode, (short)errorCode },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.DebugMessage, debugMessage },
                            { (byte)Protocol.Communication.ResponseParameters.Scene.ContainerResponseParameterCode.Parameters, parameters }
                        };
                        container.Entity.LocatedScene.SceneResponseManager.SendResponse(SceneOperationCode.ContainerOperation, ErrorCode.NoError, null, operationData);
                    }
                    break;
                default:
                    LibraryLog.ErrorFormat("Not Exist Channel for Container Communication, Channel: {0}", channel);
                    break;
            }
        }
    }
}
