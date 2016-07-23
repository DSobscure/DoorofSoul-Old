using System;
using DoorofSoul.Library.General;
using System.Collections.Generic;
using DoorofSoul.Database;

namespace DoorofSoul.Library
{
    public class Nature
    {
        #region Entity
        private Dictionary<int, Entity> entityDictionary;
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
        #endregion
        #region Scene
        private Dictionary<int, Scene> sceneDictionary;
        public bool ContainsScene(int sceneID)
        {
            return sceneDictionary.ContainsKey(sceneID);
        }
        public Scene FindScene(int sceneID)
        {
            if (ContainsScene(sceneID))
            {
                return sceneDictionary[sceneID];
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region world
        private Dictionary<int, World> worldDictionary;
        public bool ContainsWorld(int worldID)
        {
            return worldDictionary.ContainsKey(worldID);
        }
        public World FindWorld(int worldID)
        {
            if (ContainsWorld(worldID))
            {
                return worldDictionary[worldID];
            }
            else
            {
                return null;
            }
        }
        #endregion

        public Nature()
        {
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
            worldDictionary = new Dictionary<int, World>();
            LoadNature();
        }

        protected void LoadNature()
        {
            List<World> worldList = DataBase.Instance.RepositoryManager.WorldRepository.List();
            foreach (World world in worldList)
            {
                worldDictionary.Add(world.WorldID, world);
                List<Scene> sceneList = DataBase.Instance.RepositoryManager.SceneRepository.ListOfWorld(world.WorldID);
                world.LoadScenes(sceneList);
                foreach(Scene scene in sceneList)
                {
                    sceneDictionary.Add(scene.SceneID, scene);
                }
            }
        }

        public void ProjectEntity(Entity entity)
        {
            if (!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
                if(sceneDictionary.ContainsKey(entity.LocatedSceneID))
                {
                    Scene scene = sceneDictionary[entity.LocatedSceneID];
                    if (worldDictionary.ContainsKey(scene.WorldID))
                    {
                        worldDictionary[scene.WorldID].EntityEnter(entity);
                    }
                    else
                    {
                        Hexagram.Instance.Log.ErrorFormat("Hexagram: World Not Exist WorldID: {0}", scene.WorldID);
                    }
                }
                else
                {
                    Hexagram.Instance.Log.ErrorFormat("Hexagram: Scene Not Exist SceneID: {0}", entity.LocatedSceneID);
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
