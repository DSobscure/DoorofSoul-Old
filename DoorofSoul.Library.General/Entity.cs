using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Library.General.IControllers;

namespace DoorofSoul.Library.General
{
    public class Entity
    {
        #region properties
        public int EntityID { get; protected set; }
        public string EntityName { get; protected set; }
        public int LocatedSceneID { get; set; }
        public Scene LocatedScene { get; set; }

        public EntitySpaceProperties SpaceProperties { get; protected set; }

        public DSVector3 Position
        {
            get { return SpaceProperties.Position; }
            protected set { SpaceProperties.Position = value; }
        }
        public DSVector3 Rotation
        {
            get { return SpaceProperties.Rotation; }
            protected set { SpaceProperties.Rotation = value; }
        }
        public DSVector3 Scale
        {
            get { return SpaceProperties.Scale; }
            protected set { SpaceProperties.Scale = value; }
        }

        public DSVector3 Velocity
        {
            get { return SpaceProperties.Velocity; }
            protected set { SpaceProperties.Velocity = value; }
        }
        public DSVector3 MaxVelocity
        {
            get { return SpaceProperties.MaxVelocity; }
            protected set { SpaceProperties.MaxVelocity = value; }
        }
        public DSVector3 AngularVelocity
        {
            get { return SpaceProperties.AngularVelocity; }
            protected set { SpaceProperties.AngularVelocity = value; }
        }
        public DSVector3 MaxAngularVelocity
        {
            get { return SpaceProperties.MaxAngularVelocity; }
            protected set { SpaceProperties.MaxAngularVelocity = value; }
        }
        public float Mass
        {
            get { return SpaceProperties.Mass; }
            protected set { SpaceProperties.Mass = value; }
        }
        public SupportLauguages UsingLanguage { get { return LocatedScene.UsingLanguage; } }
        protected IEntityController entityController;
        public IEntityController EntityController { get { return entityController; } }
        #endregion
        #region events
        private event Action<Entity> onEntityTranformChange;
        public event Action<Entity> OnEntityTranformChange { add { onEntityTranformChange += value; } remove { onEntityTranformChange -= value; } }
        private event Action<Entity> onEntityVelocityChange;
        public event Action<Entity> OnEntityVelocityChange { add { onEntityVelocityChange += value; } remove { onEntityVelocityChange -= value; } }
        #endregion

        #region communication
        public EntityEventManager EntityEventManager { get; set; }
        public EntityOperationManager EntityOperationManager { get; set; }
        internal EntityResponseManager EntityResponseManager { get; set; }
        #endregion

        public Entity(int entityID, string entityName, int locatedSceneID, EntitySpaceProperties spaceProperties)
        {
            EntityID = entityID;
            EntityName = entityName;
            LocatedSceneID = locatedSceneID;
            SpaceProperties = spaceProperties;
            EntityEventManager = new EntityEventManager(this);
            EntityOperationManager = new EntityOperationManager(this);
            EntityResponseManager = new EntityResponseManager(this);
        }

        public void UpdateEntityTransform(DSVector3 position, DSVector3 rotation, DSVector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            onEntityTranformChange?.Invoke(this);
        }
        public void UpdateEntityVelocity(DSVector3 velocity, DSVector3 angularVelocity)
        {
            Velocity = velocity;
            AngularVelocity = angularVelocity;
            onEntityVelocityChange?.Invoke(this);
        }

        public void BindEntityController(IEntityController entityController)
        {
            this.entityController = entityController;
            entityController.BindEntity(this);
        }
    }
}
