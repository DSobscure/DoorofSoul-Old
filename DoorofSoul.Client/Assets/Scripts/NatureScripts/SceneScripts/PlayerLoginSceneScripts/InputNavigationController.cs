using DoorofSoul.Client.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts.PlayerLoginSceneScripts
{
    public class InputNavigationController : MonoBehaviour, IEventProvider
    {
        void Start()
        {
            RegisterEvents();
        }
        void OnDestroy()
        {
            EraseEvents();
        }

        private void OnKeyUp(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Tab)
            {
                var next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                if (next != null)
                {
                    EventSystem.current.SetSelectedGameObject(next.gameObject);
                    next.Select();
                }
            }
        }

        public void RegisterEvents()
        {
            Global.Global.InputManager.OnKeyUp += OnKeyUp;
        }

        public void EraseEvents()
        {
            Global.Global.InputManager.OnKeyUp -= OnKeyUp;
        }
    }
}
