using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Responses.Handlers.Player.FetchData
{
    public class FetchSceneResponseHandler : FetchDataResponseHandler
    {
        public FetchSceneResponseHandler(General.Player player) : base(player)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 3)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch Scene Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.NotExist:
                    {
                        LibraryInstance.ErrorFormat("Fetch Scene Response Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Not Exist"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Fetch Scene NotExist"]);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch Scene Response Error DebugMessage: {0}", debugMessage);
                        player.PlayerEventManager.ErrorInform(LauguageDictionarySelector.Instance[player.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[player.UsingLanguage]["Fetch Scene Error"]);
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
                    int sceneID = (int)parameters[(byte)FetchSceneResponseParameterCode.SceneID];
                    string sceneName = (string)parameters[(byte)FetchSceneResponseParameterCode.SceneName];
                    int worldID = (int)parameters[(byte)FetchSceneResponseParameterCode.WorldID];
                    player.PlayerResponseManager.FetchSceneResponse(sceneID, sceneName, worldID);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch Scene Response Parameter Cast Error");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
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
