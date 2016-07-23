using UnityEngine;
using System.Collections;
using DoorofSoul.Client.Interfaces;
using System;
using DoorofSoul.Client.Library.General;

public class EntityController : MonoBehaviour, IEntityController
{
    protected ClientEntity entity;
    public ClientEntity Entity
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

    public void BindEntity(ClientEntity entity)
    {
        this.entity = entity;
    }
}
