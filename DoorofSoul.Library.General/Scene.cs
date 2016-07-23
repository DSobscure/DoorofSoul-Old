using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Scene
    {
        public int SceneID { get; protected set; }
        public string SceneName { get; protected set; }
        public int WorldID { get; protected set; }
        protected Dictionary<int, Entity> entityDictionary;
        public IEnumerable<Entity> Entities { get { return entityDictionary.Values; } }

        private event Action<Entity> onEntityEnter;
        public event Action<Entity> OnEntityEnter { add { onEntityEnter += value; } remove { onEntityEnter -= value; } }

        private event Action<Entity> onEntityExit;
        public event Action<Entity> OnEntityExit { add { onEntityExit += value; } remove { onEntityExit -= value; } }

        public Scene(int sceneID, string sceneName, int worldID)
        {
            SceneID = sceneID;
            SceneName = sceneName;
            WorldID = worldID;
            entityDictionary = new Dictionary<int, Entity>();
        }

        public void EntityEnter(Entity entity)
        {
            if(!entityDictionary.ContainsKey(entity.EntityID))
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
                Entity entity = entityDictionary[entityID];
                entityDictionary.Remove(entity.EntityID);
                onEntityExit?.Invoke(entity);
                entity.LocatedSceneID = -1;
                entity.LocatedScene = null;
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
    }
}
