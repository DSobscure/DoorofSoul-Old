using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Container
{
    internal class SayHandler : ContainerOperationHandler
    {
        internal SayHandler(NatureComponents.Container container) : base(container, 1)
        {
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    string message = (string)parameters[(byte)SayParameterCode.Message];
                    container.Entity.LocatedScene.SceneEventManager.BroadcastMessage(MessageTypeCode.TalkMessage, MessageSourceTypeCode.Scene, container.ContainerName, message);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Container Say Operation Invalid Cast!");
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
