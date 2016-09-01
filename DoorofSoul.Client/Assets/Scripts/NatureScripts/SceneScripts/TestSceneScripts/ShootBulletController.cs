using UnityEngine;
using System.Collections;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.TestSceneScripts
{
    class ShootBulletController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody bulletPrefab;

        void Start()
        {
            Physics.gravity = new Vector3(0, -2, 0);
            Global.Global.InputManager.OnKeyDown += OnSpaceDown;
            Global.Global.Horizon.MainScene.OnShootABullet += InstantiateBullet;
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
                if (Global.Global.IsObserver)
                {
                    StartCoroutine(DestroyBullet(bullet.gameObject, bulletID));
                }
            }
        }
        private IEnumerator DestroyBullet(GameObject bullet, int bulletID)
        {
            yield return new WaitForSeconds(1);
            Destroy(bullet);
        }
    }
}
