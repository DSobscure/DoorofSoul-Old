using UnityEngine;
using System.Collections;

public class AngularVelocityTest : MonoBehaviour
{
	void Start ()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);
	}
}
