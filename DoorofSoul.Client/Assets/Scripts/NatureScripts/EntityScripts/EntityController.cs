using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using UnityEngine;

namespace DoorofSoul.Client.Scripts.NatureScripts.EntityScripts
{
    public class EntityController : MonoBehaviour, IEntityController
    {
        protected Entity entity;
        protected float rotateSpeed;
        protected Rigidbody entityRigidbody;
        protected float moveSpeed;

        public Entity Entity { get { return entity; } }

        public GameObject GameObject { get { return gameObject; } }

        void Update()
        {
            if(entity != null)
            {
                entity.Position = DSVector3.Cast(entityRigidbody.transform.localPosition);
                entity.Rotation = DSVector3.Cast(entityRigidbody.transform.localRotation.eulerAngles);

                entityRigidbody.velocity = transform.TransformVector((Vector3)entity.Velocity);
                entityRigidbody.angularVelocity = transform.TransformVector((Vector3)entity.AngularVelocity);
            }
        }
        void OnMouseEnter()
        {
            Color originEmissionColor = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", originEmissionColor + new Color(0.2f, 0.2f, 0.2f));
        }
        void OnMouseExit()
        {
            Color originEmissionColor = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", originEmissionColor - new Color(0.2f, 0.2f, 0.2f));
        }

        public void BindEntity(Entity entity)
        {
            this.entity = entity;
            entityRigidbody = GetComponent<Rigidbody>();
            rotateSpeed = 1;
            moveSpeed = 1;
            entityRigidbody.velocity = (Vector3)entity.Velocity;
            entityRigidbody.angularVelocity = (Vector3)entity.AngularVelocity;
            entity.OnSynchronizePosition += OnSynchronizePosition;
            entity.OnSynchronizeRotation += OnSynchronizeRotation;
        }

        public void StartRotate(float angularVelocity)
        {
            entityRigidbody.angularVelocity = new Vector3(0, angularVelocity, 0);
            Entity.AngularVelocity = DSVector3.Cast(transform.InverseTransformVector(entityRigidbody.angularVelocity));
        }
        public void StartMove(float velocity)
        {
            entityRigidbody.velocity = entityRigidbody.transform.forward * velocity;
            Entity.Velocity = DSVector3.Cast(transform.InverseTransformVector(entityRigidbody.velocity));
        }

        private void OnSynchronizePosition(DSVector3 position)
        {
            entityRigidbody.transform.localPosition = (Vector3)position;
        }
        private void OnSynchronizeRotation(DSVector3 rotation)
        {
            entityRigidbody.transform.localEulerAngles = (Vector3)rotation;
        }
    }
}
