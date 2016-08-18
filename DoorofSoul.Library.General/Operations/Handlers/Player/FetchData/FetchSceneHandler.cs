using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataParameters.Player;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Player;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Player.FetchData
{
    public class FetchSceneHandler : FetchDataHandler
    {
        public FetchSceneHandler(General.Player player) : base(player, 1)
        {
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    int sceneID = (int)parameter[(byte)FetchSceneParameterCode.SceneID];
                    General.Scene scene = player.PlayerCommunicationInterface.FindScene(sceneID);
                    if (scene != null)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchSceneResponseParameterCode.SceneID, scene.SceneID },
                            { (byte)FetchSceneResponseParameterCode.SceneName, scene.SceneName },
                            { (byte)FetchSceneResponseParameterCode.WorldID, scene.WorldID }
                        };
                        SendResponse(fetchCode, result);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Fetch Scene Not Exist!");
                        SendError(fetchCode, ErrorCode.NotExist, "Scene not exist");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch Scene Invalid Cast!");
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
