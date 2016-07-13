﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Server.Operations;
using Photon.SocketServer;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventParameters;
using DoorofSoul.Protocol.Communication.InformDataParameters;

namespace DoorofSoul.Server.Operations.Handlers
{
    public abstract class FetchDataHandler
    {
        protected Peer peer;

        protected FetchDataHandler(Peer peer)
        {
            this.peer = peer;
        }

        public virtual bool Handle(FetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            string debugMessage;
            if (CheckParameter(parameter, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError((byte)fetchCode, ErrorCode.ParameterError, debugMessage, LauguageDictionarySelector.Instance[peer.UsingLanguage]["Fetch Operation Parameter Error"]);
                return false;
            }
        }
        public abstract bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage);
        public void SendError(byte fetchCode, ErrorCode errorCode, string debugMessage, string errorMessage)
        {
            EventData eventData = new EventData
            {
                Code = (byte)EventCode.InformData,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)InformDataEventParameterCode.InformCode, InformDataCode.FetchDataError },
                    { (byte)InformDataEventParameterCode.ReturnCode, errorCode },
                    { (byte)InformDataEventParameterCode.Parameters, new Dictionary<byte, object>
                    {
                        { (byte)InformFetchDataError.FetchDataCode, fetchCode },
                        { (byte)InformFetchDataError.DebugMessage, debugMessage },
                        { (byte)InformFetchDataError.ErrorMessage, errorMessage }
                    }
                    }
                }
            };
            Application.Log.ErrorFormat("Error On Fetch Operation: {0}, ErrorCode:{1}, Debug Message: {2}", (FetchDataCode)fetchCode, errorCode, debugMessage);
            peer.SendEvent(eventData, new SendParameters());
        }
        public void SendEvent(byte informCode, Dictionary<byte, object> parameter)
        {
            EventData eventData = new EventData
            {
                Code = (byte)EventCode.InformData,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)InformDataEventParameterCode.InformCode, informCode },
                    { (byte)InformDataEventParameterCode.ReturnCode, ErrorCode.NoError },
                    { (byte)InformDataEventParameterCode.Parameters, parameter }
                }
            };
            peer.SendEvent(eventData, new SendParameters());
        }
    }
}