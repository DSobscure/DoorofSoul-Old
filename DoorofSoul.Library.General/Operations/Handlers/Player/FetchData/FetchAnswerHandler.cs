using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Player.FetchData
{
    public class FetchAnswerHandler : FetchDataHandler
    {
        public FetchAnswerHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Player Fetch Answer Parameter Error Parameter Count: {0}", parameter.Count);
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
                    General.Answer answer;
                    player.FetchAnswer(out answer);
                    var result = new Dictionary<byte, object>
                    {
                        { (byte)InformAnswerParameterCode.AnswerID, answer.AnswerID },
                        { (byte)InformAnswerParameterCode.SoulCountLimit, answer.SoulCountLimit }
                    };
                    SendEvent(PlayerInformDataCode.Answer, result);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("Fetch Answer Invalid Cast!");
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.Error(ex.Message);
                    LibraryLog.Error(ex.StackTrace);
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
