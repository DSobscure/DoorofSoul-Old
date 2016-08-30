using UnityEngine;
using System.Collections;

namespace Assets.Scripts.HistoryScripts.TestScripts.UnityFunctionTestScripts
{
    class ShootBulletAndBounce : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody bulletPrefab;

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        private void Shoot()
        {
            Rigidbody bullet = Instantiate(bulletPrefab);
            bullet.transform.SetParent(transform.parent);
            bullet.transform.localPosition = transform.localPosition;
            bullet.velocity = transform.forward * 10;
            StartCoroutine(DestroyBullet(bullet.gameObject));
        }

        IEnumerator DestroyBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(1);
            Destroy(bullet);
        }
    }
}
