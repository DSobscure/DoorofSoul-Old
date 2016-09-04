using DoorofSoul.Library.General.BasicTypeHelpers;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformShooterBulletSpeedChangeHandler : InformDataHandler
    {
        internal InformShooterBulletSpeedChangeHandler(NatureComponents.Scene scene) : base(scene, 2)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int containerID = (int)parameters[(byte)InformShooterBulletSpeedChangeParameterCode.ContainerID];
                    int speed = (int)parameters[(byte)InformShooterBulletSpeedChangeParameterCode.Speed];
                    if (scene.ContainsContainer(containerID))
                    {
                        NatureComponents.Container container = scene.FindContainer(containerID);
                        container.ShooterAbilities.BulletSpeed = speed;
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("InformShooterBulletSpeedChange Event Parameter Cast Error");
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
