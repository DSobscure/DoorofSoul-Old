using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public class Bullet
    {
        static int bulletCounter = 0;
        public int BulletID { get; protected set; }
        public int ShooterContainerID { get; protected set; }

        public Bullet(int shooterContainerID)
        {
            bulletCounter++;
            BulletID = bulletCounter;
            ShooterContainerID = shooterContainerID;
        }
        public Bullet(int shooterContainerID, int bulletID)
        {
            ShooterContainerID = shooterContainerID;
            BulletID = bulletID;
        }

    }
}
