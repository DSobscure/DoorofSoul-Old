using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Library
{
    public class World
    {
        public int WorldID { get; protected set; }
        public string WorldName { get; protected set; }
        protected Dictionary<int, Entity> entityDictionary;
        protected Dictionary<int, Scene> sceneDictionary;

        public World(int worldID, string worldName)
        {
            WorldID = worldID;
            WorldName = worldName;
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
        }

        public void LoadScenes(List<Scene> sceneList)
        {
            foreach (Scene scene in sceneList)
            {
                sceneDictionary.Add(scene.SceneID, scene);
            }
        }

        public void EntityEnter(Entity entity)
        {
            if(!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
            }
            if(sceneDictionary.ContainsKey(entity.LocatedSceneID))
            {
                sceneDictionary[entity.LocatedSceneID].EntityEnter(entity);
            }
        }
        public void EntityExit(Entity entity)
        {
            if (sceneDictionary.ContainsKey(entity.LocatedSceneID))
            {
                sceneDictionary[entity.LocatedSceneID].EntityExit(entity.EntityID);
            }
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Remove(entity.EntityID);
            }
        }
    }
}
