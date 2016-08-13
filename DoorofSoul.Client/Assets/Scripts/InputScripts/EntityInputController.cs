using UnityEngine;
using DoorofSoul.Library.General;

namespace DoorofSoul.Client.Scripts.InputScripts
{
    public class EntityInputController : MonoBehaviour
    {
        private bool HasEntity
        {
            get { return Global.Global.Seat != null && Global.Global.Seat.MainContainer != null && Global.Global.Seat.MainContainer.Entity != null; }
        }
        private Entity Entity
        {
            get { return Global.Global.Seat.MainContainer.Entity; }
        }

        void Update()
        {
            if (HasEntity)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Entity.EntityOperationManager.Rotate(-1);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Entity.EntityOperationManager.Rotate(1);
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    Entity.EntityOperationManager.Rotate(0);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    Entity.EntityOperationManager.Move(1);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Entity.EntityOperationManager.Move(-1);
                }
                else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                {
                    Entity.EntityOperationManager.Move(0);
                }
            }
        }
    }
}
