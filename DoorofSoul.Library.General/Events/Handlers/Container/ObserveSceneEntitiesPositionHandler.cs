using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.EventCodes;

namespace DoorofSoul.Library.General.Events.Handlers.Container
{
    internal class ObserveSceneEntitiesPositionHandler : ContainerEventHandler
    {
        public ObserveSceneEntitiesPositionHandler(General.Container container) : base(container)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("ObserveSceneEntitiesPosition Event Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    foreach(General.Entity entity in container.Entity.LocatedScene.Entities)
                    {
                        container.ContainerOperationManager.ObserveEntityPosition(entity);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ObserveSceneEntitiesPosition Event Parameter Cast Error");
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
