﻿using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;

namespace DoorofSoul.Library.General.Operations.Handlers.Container
{
    internal class SayHandler : ContainerOperationHandler
    {
        internal SayHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Container Say Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(ContainerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                string message = (string)parameters[(byte)SayParameterCode.Message];
                container.Entity.LocatedScene.SceneEventManager.BroadcastMessage(MessageType.TalkMessage, MessageSourceType.Scene, container.ContainerName, message);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
