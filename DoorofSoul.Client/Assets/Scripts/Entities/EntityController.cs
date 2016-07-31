using UnityEngine;
using System.Collections;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General.IControllers;
using DoorofSoul.Library.General;
using System;

public class EntityController : MonoBehaviour, IEntityController
{
    protected Entity entity;
    protected float rotateSpeed;
    protected Rigidbody rigidbody;
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

    public void BindEntity(Entity entity)
    {
        this.entity = entity;
        rigidbody = GetComponent<Rigidbody>();
        rotateSpeed = 1;
        moveSpeed = 1;
        rigidbody.velocity = (Vector3)entity.Velocity;
        rigidbody.angularVelocity = (Vector3)entity.AngularVelocity;
    }

    public void StartRotate(int direction)
    {
        rigidbody.angularVelocity = new Vector3(0, direction * rotateSpeed, 0);
    }
    public void StartMove(int direction)
    {
        rigidbody.velocity = rigidbody.transform.forward * direction * moveSpeed;
    }
}
