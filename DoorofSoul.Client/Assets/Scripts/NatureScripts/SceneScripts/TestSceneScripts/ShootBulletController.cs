using UnityEngine;
using System.Collections.Generic;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.TestSceneScripts
{
    class ShootBulletController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody bulletPrefab;
        Dictionary<int, Rigidbody> bulletRigidBobyDictionary;

        void Start()
        {
            bulletRigidBobyDictionary = new Dictionary<int, Rigidbody>();
            Physics.gravity = new Vector3(0, -2, 0);
            Global.Global.InputManager.OnKeyDown += OnSpaceDown;
            Global.Global.Horizon.MainScene.BulletManager.OnShootABullet += InstantiateBullet;
            Global.Global.Horizon.MainScene.BulletManager.OnDestroyBullet += DestroyBullet;
        }

        private void OnSpaceDown(KeyCode keyCode)
        {
            if(keyCode == KeyCode.Space)
            {
                Global.Global.Seat.MainContainer.ContainerOperationManager.ShootaBullet();
            }
        }
        private void InstantiateBullet(int shooterContainerID, int bulletID)
        {
            var container = Global.Global.Horizon.MainScene.FindContainer(shooterContainerID);
            if(container != null && container.ContainerController != null)
            {
                Rigidbody bullet = Instantiate(bulletPrefab);
                bullet.transform.SetParent(container.ContainerController.GameObject.transform.parent);
                bullet.transform.position = container.ContainerController.GameObject.transform.FindChild("BulletPort").position;
                bullet.velocity = container.ContainerController.GameObject.transform.forward * 10;
                bulletRigidBobyDictionary.Add(bulletID, bullet);
            }
        }
        private void DestroyBullet(int bulletID)
        {
            if(bulletRigidBobyDictionary.ContainsKey(bulletID))
            {
                Destroy(bulletRigidBobyDictionary[bulletID].gameObject);
            }
        }
    }
}
