﻿using DoorofSoul.Protocol.Communication;
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

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            if (returnCode == ErrorCode.NoError)
            {
                if (parameters.Count != 5)
                {
                    LibraryLog.ErrorFormat("Answer OperationResponse Parameter Error Parameter Count: {0}", parameters.Count);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                LibraryLog.ErrorFormat("AnswerOperationResponse Error ErrorCode: {0}, DebugMessage: {1}", returnCode, debugMessage);
                return false;
            }
        }

        public override bool Handle(PlayerOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                try
                {
                    int answerID = (int)parameters[(byte)AnswerResponseParameterCode.AnswerID];
                    AnswerOperationCode resolvedOperationCode = (AnswerOperationCode)parameters[(byte)AnswerResponseParameterCode.OperationCode];
                    ErrorCode resolvedReturnCode = (ErrorCode)parameters[(byte)AnswerResponseParameterCode.ReturnCode];
                    string resolvedDebugMessage = (string)parameters[(byte)AnswerResponseParameterCode.DebugMessage];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)AnswerResponseParameterCode.Parameters];
                    if (player.AnswerID == answerID)
                    {
                        player.Answer.AnswerResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
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
