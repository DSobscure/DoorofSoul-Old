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
        public int Damage { get; protected set; }
        public int Speed { get; protected set; }

        public Bullet(int shooterContainerID, int damage, int speed)
        {
            bulletCounter++;
            BulletID = bulletCounter;
            ShooterContainerID = shooterContainerID;
            Damage = damage;
            Speed = speed;
        }
        public Bullet(int shooterContainerID, int bulletID, int damage, int speed)
        {
            ShooterContainerID = shooterContainerID;
            BulletID = bulletID;
            Damage = damage;
            Speed = speed;
        }

    }
}
