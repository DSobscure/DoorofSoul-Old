using UnityEngine;

namespace DoorofSoul.Client.Scripts.HistoryScripts.TestScripts.UnityFunctionTestScripts
{
    public class SuspandAndClick : MonoBehaviour
    {
        private Vector3 originPosition;
        void Start()
        {
            originPosition = transform.localPosition;
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
            transform.Rotate(Time.deltaTime * Vector3.up * 25);
            transform.localPosition = originPosition + (Vector3.up * Mathf.Sin(Time.time) * 0.05f);
        }
        void OnMouseUpAsButton()
        {
            Debug.Log("Click!");
        }
    }
}
