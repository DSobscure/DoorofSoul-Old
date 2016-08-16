using UnityEngine;

namespace DoorofSoul.Client.Scripts.CameraScripts
{
    public class ViewController : MonoBehaviour
    {
        public Vector3 viewPortTilt;
        private Camera viewCamera;
        private float distanceOfEye;
        public float DistanceOfEye
        {
            get { return distanceOfEye; }
            set
            {
                distanceOfEye = Mathf.Max(Mathf.Min(value, 4), -6);
            }
        }

        void Start()
        {
            BindCamera(Camera.main);
        }
        void Update()
        {
            DistanceOfEye -= Input.GetAxis("Mouse ScrollWheel");
        }
        void OnGUI()
        {
            if (Input.GetMouseButton(2) && MouseInScreen())
            {
                Vector2 mousePosition = Event.current.mousePosition;
                float horizontal = (mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 90;
                float vertical = (mousePosition.y - Screen.height / 2) / (Screen.height / 2) * 90;
                Camera.main.transform.localRotation = Quaternion.Euler(Mathf.Max(Mathf.Min(vertical, 50), -50), Mathf.Max(Mathf.Min(horizontal, 50), -50), 0);
                Camera.main.transform.localPosition = viewPortTilt * -DistanceOfEye;
            }
        }

        public void BindCamera(Camera camera)
        {
            viewCamera = camera;
        }
        bool MouseInScreen()
        {
            Vector2 mousePosition = Event.current.mousePosition;

            if (mousePosition.x < 0 || mousePosition.x > Screen.width)
            {
                return false;
            }
            else if (mousePosition.y < 0 || mousePosition.y > Screen.height)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
