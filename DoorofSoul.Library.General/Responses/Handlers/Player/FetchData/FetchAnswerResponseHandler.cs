using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Library.General.Responses.Handlers.Player.FetchData
{
    public class FetchAnswerResponseHandler : FetchDataResponseHandler
    {
        public FetchAnswerResponseHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 2)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch Answer Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Answer Response Error DebugMessage: {0}", debugMessage);
                        player.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Fetch Answer Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int answerID = (int)parameters[(byte)FetchAnswerResponseParameterCode.AnswerID];
                    int soulCountLimit = (int)parameters[(byte)FetchAnswerResponseParameterCode.SoulCountLimit];
                    General.Answer answer = new General.Answer(answerID, soulCountLimit, player);
                    player.ActiveAnswer(answer);
                    answer.FetchSouls();
                    answer.FetchContainers();
                    answer.FetchSoulContainerLinks();
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Answer Response Parameter Cast Error");
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
