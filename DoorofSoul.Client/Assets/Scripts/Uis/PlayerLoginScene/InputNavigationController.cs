using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputNavigationController : MonoBehaviour
{
	void Update ()
    {
	    if(Input.GetKeyUp(KeyCode.Tab))
        {
            var next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                EventSystem.current.SetSelectedGameObject(next.gameObject);
                next.Select();
            }
        }
	}
}
