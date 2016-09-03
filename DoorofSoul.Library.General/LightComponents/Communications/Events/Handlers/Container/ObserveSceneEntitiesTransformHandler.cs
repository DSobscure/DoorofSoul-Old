using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication.EventCodes;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Container
{
    internal class ObserveSceneEntitiesTransformHandler : ContainerEventHandler
    {
        public ObserveSceneEntitiesTransformHandler(NatureComponents.Container container) : base(container, 0)
        {
        }

        internal override bool Handle(ContainerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(eventCode, parameters))
            {
                try
                {
                    foreach (NatureComponents.Entity entity in container.Entity.LocatedScene.Entities)
                    {
                        container.ContainerOperationManager.ObserveEntityTransform(entity);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("ObserveSceneEntitiesTransform Event Parameter Cast Error");
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
