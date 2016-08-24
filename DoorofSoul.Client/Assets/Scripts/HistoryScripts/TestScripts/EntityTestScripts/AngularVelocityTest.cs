using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.EntityTestScripts
{
    public class AngularVelocityTest : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Rigidbody>().angularDrag = 0;
            GetComponent<Rigidbody>().maxAngularVelocity = 1000000f;
        }
        void Update()
        {
            GetComponent<Rigidbody>().AddTorque(new Vector3(0, 1, 0), ForceMode.Force);
        }
    }
}
