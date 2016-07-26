using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.ResponseParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Player
{
    public class AnswerOperationResponseResolver : PlayerResponseHandler
    {
        public AnswerOperationResponseResolver(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 5)
            {
                debugMessage = string.Format("Answer OperationResponse Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int answerID = (int)parameters[(byte)AnswerResponseParameterCode.AnswerID];
                    AnswerOperationCode resolvedOperationCode = (AnswerOperationCode)parameters[(byte)AnswerResponseParameterCode.OperationCode];
                    ErrorCode returnCode = (ErrorCode)parameters[(byte)AnswerResponseParameterCode.ReturnCode];
                    string debugMessage = (string)parameters[(byte)AnswerResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)AnswerResponseParameterCode.Parameters];
                    if (player.AnswerID == answerID)
                    {
                        player.Answer.AnswerResponseManager.Operate(resolvedOperationCode, returnCode, debugMessage, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("AnswerOperationResponse Error Answer ID: {0} Not in Player ID: {1}", answerID, player.PlayerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("AnswerOperationResponse Parameter Cast Error");
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
