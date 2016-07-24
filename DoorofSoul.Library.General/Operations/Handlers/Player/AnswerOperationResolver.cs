using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;

namespace DoorofSoul.Library.General.Operations.Handlers.Player
{
    public class AnswerOperationResolver : PlayerOperationHandler
    {
        public AnswerOperationResolver(General.Player player) : base(player)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Answer Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                    int answerID = (int)parameters[(byte)AnswerOperationParameterCode.AnswerID];
                    AnswerOperationCode resolvedOperationCode = (AnswerOperationCode)parameters[(byte)AnswerOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedOperationParameters = (Dictionary<byte, object>)parameters[(byte)AnswerOperationParameterCode.Parameters];
                    if (player.AnswerID == answerID)
                    {
                        player.Answer.AnswerOperationManager.Operate(resolvedOperationCode, resolvedOperationParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("AnswerOperation Error Answer ID: {0} Not in Player ID: {1}", answerID, player.PlayerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("AnswerOperation Parameter Cast Error");
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
