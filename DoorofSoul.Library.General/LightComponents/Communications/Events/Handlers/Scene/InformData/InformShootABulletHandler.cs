using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol.Communication.InformDataCodes;
using DoorofSoul.Protocol.Communication.InformDataParameters.Scene;

namespace DoorofSoul.Library.General.LightComponents.Communications.Events.Handlers.Scene.InformData
{
    internal class InformShootABulletHandler : InformDataHandler
    {
        internal InformShootABulletHandler(NatureComponents.Scene scene) : base(scene, 2)
        {
        }

        internal override bool Handle(SceneInformDataCode informCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(informCode, parameters))
            {
                try
                {
                    int shooterContainerID = (int)parameters[(byte)InformShootABulletParameterCode.ShooterContainerID];
                    int bulletID = (int)parameters[(byte)InformShootABulletParameterCode.BulletID];
                    scene.ShootABullet(shooterContainerID);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("InformShootABullet Event Parameter Cast Error");
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
