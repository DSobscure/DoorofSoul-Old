using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Responses.Handlers.Player.FetchData
{
    public class FetchSystemVersionResponseHandler : FetchDataResponseHandler
    {
        public FetchSystemVersionResponseHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string fetchDebugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 2)
                        {
                            LibraryLog.ErrorFormat(string.Format("Fetch SystemVersion Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch SystemVersion Response Error DebugMessage: {0}", fetchDebugMessage);
                        player.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Fetch SystemVersion Error"]);
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
                    string currentServerVersion = (string)parameters[(byte)FetchSystemVersionResponseParameterCode.CurrentServerVersion];
                    string currentClientVersion = (string)parameters[(byte)FetchSystemVersionResponseParameterCode.CurrentClientVersion];
                    player.FetchSystemVersionResponse(currentServerVersion, currentClientVersion);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch SystemVersion Response Parameter Cast Error");
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
