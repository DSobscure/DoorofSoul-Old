using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Library.General.SceneElements;
using DoorofSoul.Protocol.Language;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
{
    public class Scene
    {
        #region properties
        public int SceneID { get; protected set; }
        public string SceneName { get; protected set; }
        public int WorldID { get; protected set; }
        public World World { get; protected set; }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        protected Dictionary<int, Entity> entityDictionary;
        public IEnumerable<Entity> Entities { get { return entityDictionary.Values; } }
        public SupportLauguages UsingLanguage { get { return World.UsingLanguage; } }
        #endregion

        #region events
        private event Action<Entity> onEntityEnter;
        public event Action<Entity> OnEntityEnter { add { onEntityEnter += value; } remove { onEntityEnter -= value; } }

        private event Action<Entity> onEntityExit;
        public event Action<Entity> OnEntityExit { add { onEntityExit += value; } remove { onEntityExit -= value; } }

        private event Action<Container> onContainerEnter;
        public event Action<Container> OnContainerEnter { add { onContainerEnter += value; } remove { onContainerEnter -= value; } }

        private event Action<Container> onContainerExit;
        public event Action<Container> OnContainerExit { add { onContainerExit += value; } remove { onContainerExit -= value; } }
        #endregion

        #region communication
        public SceneEventManager SceneEventManager { get; private set; }
        public SceneOperationManager SceneOperationManager { get; private set; }
        internal SceneResponseManager SceneResponseManager { get;  private set; }
        #endregion

        public MessageLog MessageLog { get; protected set; }

        public Scene(int sceneID, string sceneName, int worldID)
        {
            SceneID = sceneID;
            SceneName = sceneName;
            WorldID = worldID;
            entityDictionary = new Dictionary<int, Entity>();
            containerDictionary = new Dictionary<int, Container>();
            SceneEventManager = new SceneEventManager(this);
            SceneOperationManager = new SceneOperationManager(this);
            SceneResponseManager = new SceneResponseManager(this);
            MessageLog = new MessageLog();
        }
        public void BindWorld(World world)
        {
            World = world;
        }

        public void ContainerEnter(Container container)
        {
            if (!ContainsContainer(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
                EntityEnter(container.Entity);
                onContainerEnter?.Invoke(container);
            }
        }
        public void EntityEnter(Entity entity)
        {
            if(!ContainsEntity(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
                entity.LocatedSceneID = SceneID;
                entity.LocatedScene = this;
                onEntityEnter?.Invoke(entity);
            }
        }
        public void EntityExit(int entityID)
        {
            if (entityDictionary.ContainsKey(entityID))
            {
                Entity entity = FindEntity(entityID);
                entityDictionary.Remove(entity.EntityID);
                onEntityExit?.Invoke(entity);
                entity.LocatedSceneID = -1;
                entity.LocatedScene = null;
            }
        }
        public void ContainerExit(int containerID)
        {
            if (containerDictionary.ContainsKey(containerID))
            {
                Container container = FindContainer(containerID);
                containerDictionary.Remove(containerID);
                onContainerExit?.Invoke(container);
                EntityExit(container.Entity.EntityID);
            }
        }
        public void UpdateEntitySpaceProperties(int entityID, EntitySpaceProperties spaceProperties)
        {
            if(entityDictionary.ContainsKey(entityID))
            {
                entityDictionary[entityID].UpdateEntityTransform(spaceProperties.position, spaceProperties.rotation, spaceProperties.scale);
                entityDictionary[entityID].UpdateEntityVelocity(spaceProperties.velocity, spaceProperties.angularVelocity);
            }
        }

        public bool ContainsEntity(int entityID)
        {
            return entityDictionary.ContainsKey(entityID);
        }
        public Entity FindEntity(int entityID)
        {
            if(ContainsEntity(entityID))
            {
                return entityDictionary[entityID];
            }
            else
            {
                return null;
            }
        }
        public bool ContainsContainer(int containerID)
        {
            return containerDictionary.ContainsKey(containerID);
        }
        public Container FindContainer(int containerID)
        {
            if (ContainsContainer(containerID))
            {
                return containerDictionary[containerID];
            }
            else
            {
                return null;
            }
        }
    }
}
