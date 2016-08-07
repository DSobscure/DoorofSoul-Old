using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Container
{
    internal abstract class FetchDataHandler
    {
        protected General.Container container;

        protected FetchDataHandler(General.Container container)
        {
            this.container = container;
        }

        internal virtual bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameter(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(fetchCode, ErrorCode.ParameterError, debugMessage);
                return false;
            }
        }
        internal abstract bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage);
        internal void SendResponse(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)ErrorCode.NoError },
                { (byte)FetchDataResponseParameterCode.DebugMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            container.ContainerResponseManager.SendResponse(ContainerOperationCode.FetchData, ErrorCode.NoError, null, eventData, ContainerCommunicationChannel.Answer);
        }
        internal void SendError(ContainerFetchDataCode fetchCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)FetchDataResponseParameterCode.DebugMessage, debugMessage },
                { (byte)FetchDataResponseParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            LibraryInstance.ErrorFormat("Error On Container Fetch Operation: {0}, ErrorCode:{1}, Debug Message: {2}", fetchCode, errorCode, debugMessage);
            container.ContainerResponseManager.SendResponse(ContainerOperationCode.FetchData, ErrorCode.NoError, null, eventData, ContainerCommunicationChannel.Answer);
        }
    }
}
