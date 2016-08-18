using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.World;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.World
{
    public class SceneOperationResolver : WorldOperationHandler
    {
        public SceneOperationResolver(NatureComponents.World world) : base(world, 3)
        {
        }

        public override bool Handle(WorldOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int sceneID = (int)parameters[(byte)SceneOperationParameterCode.SceneID];
                    SceneOperationCode resolvedOperationCode = (SceneOperationCode)parameters[(byte)SceneOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SceneOperationParameterCode.Parameters];
                    if (world.ContainsScene(sceneID))
                    {
                        world.FindScene(sceneID).SceneOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("SceneOperation Error Scene ID: {0} Not in World ID: {1}", sceneID, world.WorldID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SceneOperation Parameter Cast Error");
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
