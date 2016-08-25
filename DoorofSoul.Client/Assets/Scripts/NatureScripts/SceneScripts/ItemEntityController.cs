using UnityEngine;
using DoorofSoul.Library.General.NatureComponents.SceneElements;

namespace DoorofSoul.Client.Scripts.NatureScripts.SceneScripts
{
    class ItemEntityController : MonoBehaviour
    {
        private ItemEntity itemEntity;

        void OnMouseEnter()
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));

        }
        void OnMouseExit()
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
        void OnMouseUpAsButton()
        {
            Global.Global.Seat.MainContainer.ContainerOperationManager.PickupItemEntity(itemEntity.ItemEntityID);
        }
        void Update()
        {
            if(itemEntity != null)
            {
                transform.Rotate(Time.deltaTime * Vector3.up * 25);
                transform.localPosition = (Vector3)itemEntity.Position + (Vector3.up * Mathf.Sin(Time.time) * 0.05f);
            }
        }

        public void Initial(ItemEntity itemEntity)
        {
            this.itemEntity = itemEntity;
            gameObject.name = "ItemEntity" + itemEntity.ItemEntityID;
            transform.localPosition = (Vector3)itemEntity.Position;
        }
    }
}
