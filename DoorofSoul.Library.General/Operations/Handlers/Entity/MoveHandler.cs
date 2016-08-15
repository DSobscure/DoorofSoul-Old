﻿using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Entity;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Operations.Handlers.Entity
{
    internal class MoveHandler : EntityOperationHandler
    {
        internal MoveHandler(General.Entity entity) : base(entity)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = string.Format("Entity Move Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(EntityOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int direction = (int)parameters[(byte)MoveParameterCode.Direction];
                direction = Math.Max(Math.Min(direction, 1), -1);
                entity.EntityEventManager.StartMove(direction * 5);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
