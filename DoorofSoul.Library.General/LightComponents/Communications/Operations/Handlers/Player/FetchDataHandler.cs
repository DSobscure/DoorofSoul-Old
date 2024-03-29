﻿using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters;
using DoorofSoul.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player
{
    public abstract class FetchDataHandler
    {
        protected General.Player player;
        protected int correctParameterCount;

        protected FetchDataHandler(General.Player player, int correctParameterCount)
        {
            this.player = player;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(fetchCode, ErrorCode.ParameterError, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                debugMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                debugMessage = "";
                return true;
            }
        }
        public void SendResponse(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)ErrorCode.NoError },
                { (byte)FetchDataResponseParameterCode.DebugMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            player.PlayerResponseManager.SendResponse(PlayerOperationCode.FetchData, ErrorCode.NoError, null, eventData);
        }
        public void SendError(PlayerFetchDataCode fetchCode, ErrorCode errorCode, string debugMessage)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)errorCode },
                { (byte)FetchDataResponseParameterCode.DebugMessage, debugMessage },
                { (byte)FetchDataResponseParameterCode.Parameters, new Dictionary<byte, object>() }
            };
            LibraryInstance.ErrorFormat("Error On Player Fetch Operation: {0}, ErrorCode:{1}, Debug Message: {2}", fetchCode, errorCode, debugMessage);
            player.PlayerResponseManager.SendResponse(PlayerOperationCode.FetchData, ErrorCode.NoError, null, eventData);
        }
    }
}
