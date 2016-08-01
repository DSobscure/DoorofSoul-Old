using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;

namespace DoorofSoul.Library.General.Responses.Handlers.Player.FetchData
{
    public class FetchWorldsResponseHandler : FetchDataResponseHandler
    {
        public FetchWorldsResponseHandler(General.Player player) : base(player)
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
                            LibraryLog.ErrorFormat(string.Format("Fetch Worlds Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Worlds Response Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Fetch Worlds Error"]);
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
                    int worldID = (int)parameters[(byte)FetchWorldsResponseParameterCode.WorldID];
                    string worldName = (string)parameters[(byte)FetchWorldsResponseParameterCode.WorldName];
                    player.PlayerCommunicationInterface.LoadWorld(worldID, worldName);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Worlds Response Parameter Cast Error");
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
