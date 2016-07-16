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
        public int WorldID { get; set; }
        protected Dictionary<int, Entity> entityDictionary;
        protected Dictionary<int, Scene> sceneDictionary;

        public World()
        {
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
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
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Remove(entity.EntityID);
            }
            if (sceneDictionary.ContainsKey(entity.LocatedSceneID))
            {
                sceneDictionary[entity.LocatedSceneID].EntityExit(entity);
            }
        }
    }
}
