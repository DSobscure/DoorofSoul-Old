using UnityEngine;
using System.Collections;

namespace DoorofSoul.Client.Scripts.TestScripts.UnityFunctionTestScripts
{
    public class SuspandAndClick : MonoBehaviour
    {
        void Start()
        {
            
        }
        void OnMouseEnter()
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));

        }
        void OnMouseExit()
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
        void Update()
        {
            transform.Rotate(Time.deltaTime * Vector3.up * 50);
        }
        void OnMouseUpAsButton()
        {
            Debug.Log("Click!");
        }
    }
}
