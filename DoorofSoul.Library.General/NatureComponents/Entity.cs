using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.EntityElements;
using DoorofSoul.Library.General.LightComponents.Communications.Events.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Operations.Managers;
using DoorofSoul.Library.General.LightComponents.Communications.Responses.Managers;
using DoorofSoul.Protocol.Language;
using System;

namespace DoorofSoul.Library.General.NatureComponents
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
            set { SpaceProperties.Position = value; onEntityPositionChange?.Invoke(this); }
        }
        public DSVector3 Rotation
        {
            get { return SpaceProperties.Rotation; }
            set { SpaceProperties.Rotation = value; }
        }
        public DSVector3 Scale
        {
            get { return SpaceProperties.Scale; }
            set { SpaceProperties.Scale = value; }
        }

        public DSVector3 Velocity
        {
            get { return SpaceProperties.Velocity; }
            set { SpaceProperties.Velocity = value; }
        }
        public DSVector3 MaxVelocity
        {
            get { return SpaceProperties.MaxVelocity; }
            protected set { SpaceProperties.MaxVelocity = value; }
        }
        public DSVector3 AngularVelocity
        {
            get { return SpaceProperties.AngularVelocity; }
            set { SpaceProperties.AngularVelocity = value; }
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
        public IEntityController EntityController { get; protected set; }
        #endregion

        #region events
        private event Action<Entity> onEntityPositionChange;
        public event Action<Entity> OnEntityPositionChange { add { onEntityPositionChange += value; } remove { onEntityPositionChange -= value; } }
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

        public void BindEntityController(IEntityController entityController)
        {
            
            EntityController = entityController;
            EntityController.BindEntity(this);
        }

        public void SynchronizePosition(DSVector3 position)
        {
            Position = position;
        }
    }
}
