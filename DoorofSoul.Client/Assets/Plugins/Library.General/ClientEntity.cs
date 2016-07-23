using DoorofSoul.Library.General;
using DoorofSoul.Client.Interfaces;
using UnityEngine;

namespace DoorofSoul.Client.Library.General
{
    public class ClientEntity : Entity
    {
        protected IEntityController entityController;
        public GameObject GameObject { get { return entityController.GameObject; } }

        public ClientEntity(int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties) : base(entityID, entityName, locatedSceneID, spaceProperties)
        {

        }
        public void BindEntityController(IEntityController entityController)
        {
            this.entityController = entityController;
        }
    }
}
