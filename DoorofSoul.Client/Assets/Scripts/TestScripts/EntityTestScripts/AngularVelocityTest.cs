using UnityEngine;
using System.Collections;

namespace DoorofSoul.Client.Scripts.TestScripts.EntityTestScripts
{
    public class AngularVelocityTest : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);
        }
    }
}
