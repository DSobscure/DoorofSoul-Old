using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Scene;

namespace DoorofSoul.Library.General.Operations.Handlers.Scene
{
    internal class ContainerOperationResolver : SceneOperationHandler
    {
        internal ContainerOperationResolver(General.Scene scene) : base(scene)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Container Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(SceneOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)ContainerOperationParameterCode.ContainerID];
                    ContainerOperationCode resolvedOperationCode = (ContainerOperationCode)parameters[(byte)ContainerOperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)ContainerOperationParameterCode.Parameters];
                    if (scene.ContainsContainer(containerID))
                    {
                        scene.FindContainer(containerID).ContainerOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        LibraryLog.ErrorFormat("ContainerOperation Error Container ID: {0} Not in Scene ID: {1}", containerID, scene.SceneID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("ContainerOperation Parameter Cast Error");
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryLog.ErrorFormat(ex.Message);
                    LibraryLog.ErrorFormat(ex.StackTrace);
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
