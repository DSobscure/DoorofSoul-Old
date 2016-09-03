using UnityEngine;
using DoorofSoul.Client.Scripts.NatureScripts.ContainerScripts;

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
                    int hitContainerID = collision.gameObject.GetComponent<ContainerController>().Container.ContainerID;
                    Global.Global.Seat.MainContainer.ContainerOperationManager.ObserveBulletHit(hitContainerID, collision.impulse.magnitude);
                }
            }
        }
    }
}
