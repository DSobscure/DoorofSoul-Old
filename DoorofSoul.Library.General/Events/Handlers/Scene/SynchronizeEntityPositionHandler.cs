using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.EventParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.Events.Handlers.Scene
{
    internal class SynchronizeEntityPositionHandler : SceneEventHandler
    {
        public SynchronizeEntityPositionHandler(General.Scene scene) : base(scene)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 2)
            {
                debugMessage = string.Format("SynchronizeEntityPosition Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(SceneEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    int entityID = (int)parameters[(byte)SynchronizeEntityPositionParameterCode.EntityID];
                    DSVector3 position = (DSVector3)parameters[(byte)SynchronizeEntityPositionParameterCode.Position];
                    if(scene.ContainsEntity(entityID))
                    {
                        scene.FindEntity(entityID).SynchronizePosition(position); 
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("SynchronizeEntityPosition Event Parameter Cast Error");
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
