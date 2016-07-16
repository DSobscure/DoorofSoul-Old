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
            }
        }
        public void EntityExit(Entity entity)
        {
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Remove(entity.EntityID);
                entity.LocatedSceneID = -1;
                entity.LocatedScene = null;
            }
        }
    }
}
