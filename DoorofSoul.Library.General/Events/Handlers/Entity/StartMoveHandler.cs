﻿using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Entity;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Entity
{
    internal class StartMoveHandler : EntityEventHandler
    {
        internal StartMoveHandler(General.Entity entity) : base(entity)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count != 1)
            {
                debugMessage = string.Format("StartMove Event Parameter Error Parameter Count: {0}", parameters.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(EntityEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    float velocity = (float)parameters[(byte)StartMoveParameterCode.Velocity];
                    entity.EntityController.StartMove(velocity);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryLog.ErrorFormat("StartMove Event Parameter Cast Error");
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
