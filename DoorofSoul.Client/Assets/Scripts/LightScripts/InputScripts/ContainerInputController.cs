using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General.NatureComponents;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.LightScripts.InputScripts
{
    public class ContainerInputController : MonoBehaviour, IEventProvider
    {
        private bool HasContainer
        {
            get { return Global.Global.Seat != null && Global.Global.Seat.MainContainer != null; }
        }
        private Container Container
        {
            get { return Global.Global.Seat.MainContainer; }
        }

        void Start()
        {
            RegisterEvents();
        }
        void OnDestroy()
        {
            EraseEvents();
        }
        private void OnKeyDown(KeyCode keyCode)
        {
            if (HasContainer)
            {
                if (keyCode == KeyCode.A)
                {
                    Container.ContainerOperationManager.Rotate(-1);
                }
                if (keyCode == KeyCode.D)
                {
                    Container.ContainerOperationManager.Rotate(1);
                }
                if (keyCode == KeyCode.W)
                {
                    Container.ContainerOperationManager.Move(1);
                }
                if (keyCode == KeyCode.S)
                {
                    Container.ContainerOperationManager.Move(-1);
                }
            }
        }
        private void OnKeyUp(KeyCode keyCode)
        {
            if (HasContainer)
            {
                if (keyCode == KeyCode.A || keyCode == KeyCode.D)
                {
                    Container.ContainerOperationManager.Rotate(0);
                }
                if (keyCode == KeyCode.W || keyCode == KeyCode.S)
                {
                    Container.ContainerOperationManager.Move(0);
                }
            }
        }

        public void RegisterEvents()
        {
            Global.Global.InputManager.OnKeyDown += OnKeyDown;
            Global.Global.InputManager.OnKeyUp += OnKeyUp;
        }

        public void EraseEvents()
        {
            Global.Global.InputManager.OnKeyDown -= OnKeyDown;
            Global.Global.InputManager.OnKeyUp -= OnKeyUp;
        }
    }
}
