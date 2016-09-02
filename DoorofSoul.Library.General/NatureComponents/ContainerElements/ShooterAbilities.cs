using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public class ShooterAbilities
    {
        private int damage;
        public int Damage
        {
            get { return damage; }
            set
            {
                damage = Math.Max(Math.Min(value, 5), 0);
                onDamagehange?.Invoke(damage);
            }
        }

        private int moveSpeed;
        public int MoveSpeed
        {
            get { return moveSpeed; }
            set
            {
                moveSpeed = Math.Max(Math.Min(value, 5), 0);
                onMoveSpeedChange?.Invoke(moveSpeed);
            }
        }

        private int bulletSpeed;
        public int BulletSpeed
        {
            get { return bulletSpeed; }
            set
            {
                bulletSpeed = Math.Max(Math.Min(value, 5), 0);
                onBulletSpeedChange?.Invoke(bulletSpeed);
            }
        }

        private int transparancy;
        public int Transparancy
        {
            get { return transparancy; }
            set
            {
                transparancy = Math.Max(Math.Min(value, 5), 0);
                onTransparancyChange?.Invoke(transparancy);
            }
        }

        private event Action<int> onDamagehange;
        public event Action<int> OnDamageChange { add { onDamagehange += value; } remove { onDamagehange -= value; } }

        private event Action<int> onMoveSpeedChange;
        public event Action<int> OnMoveSpeedChange { add { onMoveSpeedChange += value; } remove { onMoveSpeedChange -= value; } }

        private event Action<int> onBulletSpeedChange;
        public event Action<int> OnBulletSpeedChange { add { onBulletSpeedChange += value; } remove { onBulletSpeedChange -= value; } }

        private event Action<int> onTransparancyChange;
        public event Action<int> OnTransparancyChange { add { onTransparancyChange += value; } remove { onTransparancyChange -= value; } }
    }
}
