using DoorofSoul.Database;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Library
{
    public class Nature
    {
        #region Container
        private Dictionary<int, Container> containerDictionary;
        public bool ContainsContainer(int container)
        {
            return containerDictionary.ContainsKey(container);
        }
        public Container FindContainer(int container)
        {
            if (ContainsContainer(container))
            {
                return containerDictionary[container];
            }
            else
            {
                return null;
            }
        }
        #endregion
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
            containerDictionary = new Dictionary<int, Container>();
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
            worldDictionary = new Dictionary<int, World>();
            LoadNature();
        }

        protected void LoadNature()
        {
            List<World> worldList = new List<World>();
            Database.Database.RepositoryList.NatureRepositoryList.WorldRepository.List().ForEach(world => 
            {
                HexagramWorldCommunicationInterface communicationInterface = new HexagramWorldCommunicationInterface();
                World newWorld = new World(communicationInterface, world.worldID, world.worldName);
                communicationInterface.BindWorld(newWorld);
                worldList.Add(newWorld);
            });
            foreach (World world in worldList)
            {
                worldDictionary.Add(world.WorldID, world);
                List<Scene> sceneList = Database.Database.RepositoryList.NatureRepositoryList.SceneRepository.ListOfWorld(world.WorldID);
                world.LoadScenes(sceneList);
                foreach(Scene scene in sceneList)
                {
                    sceneDictionary.Add(scene.SceneID, scene);
                    scene.SetSceneEye(new ProvidenceEye());
                    scene.OnEntityEnter += scene.SceneEventManager.EntityEnter;
                    scene.OnEntityExit += scene.SceneEventManager.EntityExit;
                }
            }
        }

        public void ProjectContainer(Container container)
        {
            if (!ContainsContainer(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
                if (sceneDictionary.ContainsKey(container.Entity.LocatedSceneID))
                {
                    Scene scene = sceneDictionary[container.Entity.LocatedSceneID];
                    if (worldDictionary.ContainsKey(scene.WorldID))
                    {
                        worldDictionary[scene.WorldID].ContainerEnter(container);
                        ProjectEntity(container.Entity);
                    }
                    else
                    {
                        Hexagram.Instance.Log.ErrorFormat("Hexagram: World Not Exist WorldID: {0}", scene.WorldID);
                    }
                }
                else
                {
                    Hexagram.Instance.Log.ErrorFormat("Hexagram: Scene Not Exist SceneID: {0}", container.Entity.LocatedSceneID);
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
        public void ExtractContainer(Container container)
        {
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                if (worldDictionary.ContainsKey(container.Entity.LocatedScene.WorldID))
                {
                    Database.Database.RepositoryList.NatureRepositoryList.EntityRepository.Save(container.Entity);
                    worldDictionary[container.Entity.LocatedScene.WorldID].ContainerExit(container);
                    ExtractEntity(container.Entity);
                }
                containerDictionary.Remove(container.ContainerID);
            }
        }
        public void ExtractEntity(Entity entity)
        {
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                if (entity.LocatedScene != null && worldDictionary.ContainsKey(entity.LocatedScene.WorldID))
                {
                    Database.Database.RepositoryList.NatureRepositoryList.EntityRepository.Save(entity);
                    worldDictionary[entity.LocatedScene.WorldID].EntityExit(entity);
                }
                entityDictionary.Remove(entity.EntityID);
            }
        }

        public List<World> ListWorlds()
        {
            return worldDictionary.Values.ToList();
        }
    }
}
