using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Player
{
    public class AnswerEventResolver : PlayerEventHandler
    {
        public AnswerEventResolver(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Answer Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int answerID = (int)parameters[(byte)AnswerEventParameterCode.AnswerID];
                    AnswerEventCode resolvedEventCode = (AnswerEventCode)parameters[(byte)AnswerEventParameterCode.EventCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)AnswerEventParameterCode.Parameters];
                    if (player.AnswerID == answerID)
                    {
                        player.Answer.AnswerEventManager.Operate(resolvedEventCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("AnswerEvent Error Answer ID: {0} Not in Player ID: {1}", answerID, player.PlayerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("AnswerEvent Parameter Cast Error");
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
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
