using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Container;

namespace DoorofSoul.Library.General.Operations.Handlers.Container
{
    internal class SendMessageHandler : ContainerOperationHandler
    {
        internal SendMessageHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Container SendMessage Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
            //if (base.Handle(operationCode, parameters))
            //{
            //    MessageTargetType targetType = (MessageTargetType)parameters[(byte)SendMessageParameterCode.MessageTargetType];
            //    string specificTarget = (string)parameters[(byte)SendMessageParameterCode.SpecificTarget];
            //    string message = (string)parameters[(byte)SendMessageParameterCode.Message];
            //    switch(targetType)
            //    {
            //        case MessageTargetType.Scene:
            //            return container.Entity.LocatedScene.SendMessageOperation
            //            break;
            //        case MessageTargetType.Personal:
            //            if(specificTarget != null)
            //            {
            //                return container.Entity.LocatedScene.World.SendMessageOperation
            //            }
            //            break;
            //        default:
            //            SendError(operationCode, ErrorCode.InvalidOperation, string.Format("Container SendMessage Operation Not Exist TargetType: {0}", targetType));
            //            break;
            //    }
            //}
            //else
            {
                return false;
            }
        }
    }
}
