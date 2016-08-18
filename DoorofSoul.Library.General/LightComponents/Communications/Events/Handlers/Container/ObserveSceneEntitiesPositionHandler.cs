using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.EventCodes;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container
{
    internal class ObserveSceneEntitiesPositionHandler : ContainerEventHandler
    {
        public ObserveSceneEntitiesPositionHandler(NatureComponents.Container container) : base(container, 0)
        {
        }

        internal override bool Handle(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    foreach(NatureComponents.Entity entity in container.Entity.LocatedScene.Entities)
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
