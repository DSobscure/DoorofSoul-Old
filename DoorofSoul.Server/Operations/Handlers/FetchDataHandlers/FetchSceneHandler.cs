using DoorofSoul.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.InformDataParameters;
using DoorofSoul.Protocol.Communication.FetchDataParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers.FetchDataHandlers
{
    public class FetchSceneHandler : FetchDataHandler
    {
        public FetchSceneHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("fetch scene has {0} parameters!", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(FetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    int sceneID = (int)parameter[(byte)FetchSceneParameterCode.SceneID];
                    if(Hexagram.Instance.Nature.ContainsScene(sceneID))
                    {
                        Scene scene = Hexagram.Instance.Nature.FindScene(sceneID);
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)InformSceneParameterCode.SceneID, scene.SceneID },
                            { (byte)InformSceneParameterCode.SceneName, scene.SceneName },
                            { (byte)InformSceneParameterCode.WorldID, scene.WorldID }
                        };
                        SendEvent((byte)InformDataCode.Scene, result);
                        return true;
                    }
                    else
                    {
                        Application.Log.ErrorFormat("Fetch Scene Not Exist!");
                        SendError((byte)fetchCode, ErrorCode.NotExist, "Scene not exist", null);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("Fetch Scene Invalid Cast!");
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Application.Log.Error(ex.Message);
                    Application.Log.Error(ex.StackTrace);
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
