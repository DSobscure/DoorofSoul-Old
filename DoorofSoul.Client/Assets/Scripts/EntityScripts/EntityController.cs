using DoorofSoul.Library.General;
using DoorofSoul.Library.General.IControllers;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.EntityScripts
{
    public class EntityController : MonoBehaviour, IEntityController
    {
        protected Entity entity;
        protected float rotateSpeed;
        protected Rigidbody entityRigidbody;
        protected float moveSpeed;

        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public GameObject GameObject
        {
            get
            {
                return gameObject;
            }
        }

        void OnMouseEnter()
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f));
        }
        void OnMouseExit()
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }

        public void BindEntity(Entity entity)
        {
            this.entity = entity;
            entityRigidbody = GetComponent<Rigidbody>();
            rotateSpeed = 1;
            moveSpeed = 1;
            entityRigidbody.velocity = (Vector3)entity.Velocity;
            entityRigidbody.angularVelocity = (Vector3)entity.AngularVelocity;
        }

        public void StartRotate(float velocity)
        {
            entityRigidbody.angularVelocity = new Vector3(0, velocity, 0);
        }
        public void StartMove(float angularVelocity)
        {
            entityRigidbody.velocity = entityRigidbody.transform.forward * angularVelocity;
        }
    }
}
