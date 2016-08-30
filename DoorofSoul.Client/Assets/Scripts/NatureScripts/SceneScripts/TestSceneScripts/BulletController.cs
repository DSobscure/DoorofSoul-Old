using UnityEngine;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.TestSceneScripts
{
    class BulletController : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if(Global.Global.IsObserver)
            {
                if (collision.gameObject.tag == "Container")
                {
                    Debug.Log("Hit!");
                }
            }
        }
    }
}
