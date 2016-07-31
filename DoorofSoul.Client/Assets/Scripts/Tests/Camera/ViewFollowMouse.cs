using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ViewFollowMouse : MonoBehaviour
{
    private float distance;
    public float Distance
    {
        get { return distance; }
        set
        {
            distance = Mathf.Max(Mathf.Min(value, 0), -5);
        }
    }
    void FixedUpdate()
    {
        Distance += Input.GetAxis("Mouse ScrollWheel");
    }
    void OnGUI()
    {
        if(MouseInScreen())
        {
            Vector2 mousePosition = Event.current.mousePosition;
            float horizontal = (mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 90;
            float vertical = (mousePosition.y - Screen.height / 2) / (Screen.height / 2) * 90;
            Camera.main.transform.rotation = Quaternion.Euler(Mathf.Max(Mathf.Min(vertical, 50),-50), Mathf.Max(Mathf.Min(horizontal, 50), -50), 0);
            Camera.main.transform.localPosition = new Vector3(0, 0, Distance);
        }
    }

    bool MouseInScreen()
    {
        Vector2 mousePosition = Event.current.mousePosition;

        if (mousePosition.x < 0 || mousePosition.x > Screen.width)
        {
            return false;
        }
        else if(mousePosition.y < 0 || mousePosition.y > Screen.height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
