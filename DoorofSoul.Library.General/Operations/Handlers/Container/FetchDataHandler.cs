﻿using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.Channels;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Operations.Handlers.Container
{
    public abstract class FetchDataHandler
    {
        protected General.Container container;

        protected FetchDataHandler(General.Container container)
        {
            this.container = container;
        }

        public virtual bool Handle(ContainerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(fetchCode, ErrorCode.ParameterError, debugMessage, null);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(ContainerFetchDataCode fetchCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)InformFetchDataErrorParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)InformFetchDataErrorParameterCode.DebugMessage, debugMessage },
                { (byte)InformFetchDataErrorParameterCode.ErrorMessage, errorMessage }
            };
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)ContainerInformDataCode.FetchDataError },
                { (byte)InformDataEventParameterCode.ReturnCode, (short)errorCode },
                { (byte)InformDataEventParameterCode.ReturnCode, parameters },
            };
            LibraryLog.ErrorFormat("Error On Container Fetch Operation: {0}, ErrorCode:{1}, Debug Message: {2}", fetchCode, errorCode, debugMessage);
            container.SendEvent(ContainerEventCode.InformData, eventData, ContainerCommunicationChannel.Answer);
        }
        public void SendEvent(ContainerInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)InformDataEventParameterCode.InformCode, (byte)informCode },
                { (byte)InformDataEventParameterCode.ReturnCode, (short)ErrorCode.NoError },
                { (byte)InformDataEventParameterCode.ReturnCode, parameters },
            };
            container.SendEvent(ContainerEventCode.InformData, eventData, ContainerCommunicationChannel.Answer);
        }
    }
}
