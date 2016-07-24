using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchSoulsHandler : FetchDataHandler
    {
        public FetchSoulsHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("fetch souls has {0} parameters!", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach(Soul soul in peer.Player.Answer.Souls)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)InformSoulParameterCode.SoulID, soul.SoulID },
                            { (byte)InformSoulParameterCode.AnswerID, soul.AnswerID },
                            { (byte)InformSoulParameterCode.SoulName, soul.SoulName }
                        };
                        SendEvent((byte)PlayerInformDataCode.Soul, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch Souls Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
