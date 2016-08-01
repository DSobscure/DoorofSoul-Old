using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters.World;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.World;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.World.FetchData
{
    public class FetchSceneHandler : FetchDataHandler
    {
        public FetchSceneHandler(General.World world) : base(world)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Player Fetch Scene Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(WorldFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    int sceneID = (int)parameter[(byte)FetchSceneParameterCode.SceneID];
                    General.Scene scene = world.FindScene(sceneID);
                    if (scene != null)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchSceneResponseParameterCode.SceneID, scene.SceneID },
                            { (byte)FetchSceneResponseParameterCode.SceneName, scene.SceneName },
                        };
                        SendResponse(fetchCode, result);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("Fetch Scene Not Exist!");
                        SendError(fetchCode, ErrorCode.NotExist, "Scene not exist");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("Fetch Scene Invalid Cast!");
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
