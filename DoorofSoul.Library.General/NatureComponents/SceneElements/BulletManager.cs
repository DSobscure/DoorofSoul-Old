using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General.NatureComponents.SceneElements
{
    public class BulletManager
    {
        protected Scene scene;
        protected Dictionary<int, Bullet> bulletDictionary;

        private event Action<int, int> onShootABullet;
        public event Action<int, int> OnShootABullet { add { onShootABullet += value; } remove { onShootABullet -= value; } }

        private event Action<int> onDestroyBullet;
        public event Action<int> OnDestroyBullet { add { onDestroyBullet += value; } remove { onDestroyBullet -= value; } }

        public BulletManager(Scene scene)
        {
            this.scene = scene;
            bulletDictionary = new Dictionary<int, Bullet>();
        }

        public void AddBullet(Bullet bullet)
        {
            if(scene.ContainsContainer(bullet.ShooterContainerID) && !bulletDictionary.ContainsKey(bullet.BulletID))
            {
                bulletDictionary.Add(bullet.BulletID, bullet);
                onShootABullet?.Invoke(bullet.ShooterContainerID, bullet.BulletID);
            }
        }
        public void RemoveBullet(int bulletID)
        {
            if (bulletDictionary.ContainsKey(bulletID))
            {
                onDestroyBullet?.Invoke(bulletID);
                bulletDictionary.Remove(bulletID);
            }
        }
    }
}
