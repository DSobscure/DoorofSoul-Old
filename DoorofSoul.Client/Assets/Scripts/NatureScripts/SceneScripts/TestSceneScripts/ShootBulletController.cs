using UnityEngine;
using System.Collections.Generic;
using DoorofSoul.Library.General.NatureComponents.SceneElements;

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
            Physics.gravity = new Vector3(0, -1, 0);
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
        private void InstantiateBullet(Bullet bullet)
        {
            var container = Global.Global.Horizon.MainScene.FindContainer(bullet.ShooterContainerID);
            if(container != null && container.ContainerController != null)
            {
                Rigidbody bulletRigidBody = Instantiate(bulletPrefab);
                bulletRigidBody.transform.SetParent(container.ContainerController.GameObject.transform.parent);
                bulletRigidBody.transform.position = container.ContainerController.GameObject.transform.FindChild("BulletPort").position;
                bulletRigidBody.mass = 1 + bullet.Damage;
                bulletRigidBody.GetComponent<Renderer>().material.SetColor("_Color", new Color(bullet.Damage / 5f, 0, 0, 1));
                bulletRigidBody.velocity = container.ContainerController.GameObject.transform.forward * 10 * (1 + bullet.Speed / 2f);
                bulletRigidBobyDictionary.Add(bullet.BulletID, bulletRigidBody);
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
