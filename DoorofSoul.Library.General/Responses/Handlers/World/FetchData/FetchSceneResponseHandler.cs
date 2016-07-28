using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.World;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Library.General.Responses.Handlers.World.FetchData
{
    public class FetchSceneResponseHandler : FetchDataResponseHandler
    {
        public FetchSceneResponseHandler(General.World world) : base(world)
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
                            LibraryLog.ErrorFormat(string.Format("Fetch Scene Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.NotExist:
                    {
                        LibraryLog.ErrorFormat("Fetch Scene Response Error DebugMessage: {0}", debugMessage);
                        world.ErrorInform(LauguageDictionarySelector.Instance[world.UsingLanguage]["Not Exist"], LauguageDictionarySelector.Instance[world.UsingLanguage]["Fetch Scene NotExist"]);
                        return false;
                    }
                default:
                    {
                        LibraryLog.ErrorFormat("Fetch Scene Response Error DebugMessage: {0}", debugMessage);
                        world.ErrorInform(LauguageDictionarySelector.Instance[world.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[world.UsingLanguage]["Fetch Scene Error"]);
                        return false;
                    }
            }
        }

        public override bool Handle(WorldFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    int sceneID = (int)parameters[(byte)FetchSceneResponseParameterCode.SceneID];
                    string sceneName = (string)parameters[(byte)FetchSceneResponseParameterCode.SceneName];
                    world.FetchSceneResponse(sceneID, sceneName);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.Error("Fetch Scene Response Parameter Cast Error");
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
