using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchAnswerHandler : FetchDataHandler
    {
        public FetchAnswerHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("fetch answer has {0} parameters!", parameter.Count);
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
            if(base.Handle(fetchCode, parameter))
            {
                try
                {
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)InformAnswerParameterCode.AnswerID, peer.Player.Answer.AnswerID },
                        { (byte)InformAnswerParameterCode.SoulCountLimit, peer.Player.Answer.SoulCountLimit }
                    };
                    SendEvent((byte)PlayerInformDataCode.Answer, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch Answer Invalid Cast!");
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
