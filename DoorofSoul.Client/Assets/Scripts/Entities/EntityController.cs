using UnityEngine;
using System.Collections;
using DoorofSoul.Client.Interfaces;
using DoorofSoul.Library.General.IControllers;
using DoorofSoul.Library.General;
using System;

public class EntityController : MonoBehaviour, IEntityController
{
    protected Entity entity;

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
    }
}
