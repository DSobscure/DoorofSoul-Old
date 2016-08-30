using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General.NatureComponents;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.LightScripts.InputScripts
{
    public class EntityInputController : MonoBehaviour, IEventProvider
    {
        private bool HasEntity
        {
            get { return Global.Global.Seat != null && Global.Global.Seat.MainContainer != null && Global.Global.Seat.MainContainer.Entity != null; }
        }
        private Entity Entity
        {
            get { return Global.Global.Seat.MainContainer.Entity; }
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
            if (HasEntity)
            {
                if (keyCode == KeyCode.A)
                {
                    Entity.EntityOperationManager.Rotate(-1);
                }
                if (keyCode == KeyCode.D)
                {
                    Entity.EntityOperationManager.Rotate(1);
                }
                if (keyCode == KeyCode.W)
                {
                    Entity.EntityOperationManager.Move(1);
                }
                if (keyCode == KeyCode.S)
                {
                    Entity.EntityOperationManager.Move(-1);
                }
            }
        }
        private void OnKeyUp(KeyCode keyCode)
        {
            if (HasEntity)
            {
                if (keyCode == KeyCode.A || keyCode == KeyCode.D)
                {
                    Entity.EntityOperationManager.Rotate(0);
                }
                if (keyCode == KeyCode.W || keyCode == KeyCode.S)
                {
                    Entity.EntityOperationManager.Move(0);
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
