using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Library.General
{
    public class Scene
    {
        public int SceneID { get; protected set; }
        protected Dictionary<int, Entity> entityDictionary;

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
