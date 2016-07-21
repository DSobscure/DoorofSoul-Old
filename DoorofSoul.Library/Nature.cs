using System;
using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Library
{
    public class Nature
    {
        private Dictionary<int, Entity> entityDictionary;
        private Dictionary<int, World> worldDictionary;

        public Nature()
        {
            entityDictionary = new Dictionary<int, Entity>();
            worldDictionary = new Dictionary<int, World>();
        }

        public void ProjectEntity(Entity entity)
        {
            if (!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
                if (worldDictionary.ContainsKey(entity.LocatedScene.WorldID))
                {
                    worldDictionary[entity.LocatedScene.WorldID].EntityEnter(entity);
                }
            }
        }
        public void ExtractEntity(Entity entity)
        {
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                if (worldDictionary.ContainsKey(entity.LocatedScene.WorldID))
                {
                    worldDictionary[entity.LocatedScene.WorldID].EntityExit(entity);
                }
                entityDictionary.Remove(entity.EntityID);
            }
        }
    }
}
