using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication.OperationParameters.Player;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Player
{
    internal class AnswerOperationResolver : PlayerOperationHandler
    {
        internal AnswerOperationResolver(General.Player player) : base(player, 3)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int answerID = (int)parameters[(byte)AnswerOperationParameterCode.AnswerID];
                    AnswerOperationCode resolvedOperationCode = (AnswerOperationCode)parameters[(byte)AnswerOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)AnswerOperationParameterCode.Parameters];
                    if (player.AnswerID == answerID)
                    {
                        player.Answer.AnswerOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("AnswerOperation Error Answer ID: {0} Not in Player ID: {1}", answerID, player.PlayerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("AnswerOperation Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
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
