using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Responses.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Language;
using System.Collections.Generic;

namespace DoorofSoul.Library.General
{
    public abstract class World
    {
        public int WorldID { get; protected set; }
        public string WorldName { get; protected set; }
        protected Dictionary<int, Container> containerDictionary;
        public IEnumerable<Container> Containers { get { return containerDictionary.Values; } }
        protected Dictionary<int, Entity> entityDictionary;
        public IEnumerable<Entity> Entities { get { return entityDictionary.Values; } }
        protected Dictionary<int, Scene> sceneDictionary;
        public IEnumerable<Scene> Scenes { get { return sceneDictionary.Values; } }
        public abstract SupportLauguages UsingLanguage { get; }

        #region communication
        public WorldEventManager WorldEventManager { get; protected set; }
        public WorldOperationManager WorldOperationManager { get; protected set; }
        public WorldResponseManager WorldResponseManager { get; protected set; }
        public abstract void SendEvent(WorldEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendOperation(WorldOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendResponse(WorldOperationCode operationCode, ErrorCode returnCode, string degugMessage, Dictionary<byte, object> parameters);
        public abstract void ErrorInform(string title, string message);
        public abstract void FetchScene(int sceneID, out Scene scene);
        public abstract void FetchSceneResponse(int sceneID, string sceneName);
        #endregion

        public World(int worldID, string worldName)
        {
            WorldID = worldID;
            WorldName = worldName;
            containerDictionary = new Dictionary<int, Container>();
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
            WorldEventManager = new WorldEventManager(this);
            WorldOperationManager = new WorldOperationManager(this);
            WorldResponseManager = new WorldResponseManager(this);
        }

        public void LoadScenes(List<Scene> sceneList)
        {
            foreach (Scene scene in sceneList)
            {
                if(!sceneDictionary.ContainsKey(scene.SceneID))
                {
                    sceneDictionary.Add(scene.SceneID, scene);
                    scene.BindWorld(this);
                }
            }
        }

        public void ContainerEnter(Container container)
        {
            if (!containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Add(container.ContainerID, container);
                EntityEnter(container.Entity);
            }
            if (sceneDictionary.ContainsKey(container.Entity.LocatedSceneID))
            {
                sceneDictionary[container.Entity.LocatedSceneID].ContainerEnter(container);
            }
        }
        public void EntityEnter(Entity entity)
        {
            if (!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
            }
            if (sceneDictionary.ContainsKey(entity.LocatedSceneID))
            {
                sceneDictionary[entity.LocatedSceneID].EntityEnter(entity);
            }
        }
        public void ContainerExit(Container container)
        {
            if (sceneDictionary.ContainsKey(container.Entity.LocatedSceneID))
            {
                sceneDictionary[container.Entity.LocatedSceneID].ContainerExit(container.ContainerID);
            }
            if (containerDictionary.ContainsKey(container.ContainerID))
            {
                containerDictionary.Remove(container.ContainerID);
                EntityExit(container.Entity);
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
        public bool ContainsScene(int sceneID)
        {
            return sceneDictionary.ContainsKey(sceneID);
        }
        public Scene FindScene(int sceneID)
        {
            if(ContainsScene(sceneID))
            {
                return sceneDictionary[sceneID];
            }
            else
            {
                return null;
            }
        }
    }
}
