using System;
using System.Collections.Generic;
using DoorofSoul.Library.General.Events;
using DoorofSoul.Library.General.Operations.Managers;
using DoorofSoul.Library.General.Events.Managers;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.EventCodes;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General
{
    public class World
    {
        public int WorldID { get; protected set; }
        public string WorldName { get; protected set; }
        protected Dictionary<int, Entity> entityDictionary;
        protected Dictionary<int, Scene> sceneDictionary;

        #region communication
        public WorldEventManager WorldEventManager { get; protected set; }
        public WorldOperationManager WorldOperationManager { get; protected set; }
        public Action<WorldEventCode, Dictionary<byte, object>> SendEvent { get; protected set; }
        public Action<WorldOperationCode, Dictionary<byte, object>> SendResponse { get; protected set; }
        public Action<WorldOperationCode, ErrorCode, string, Dictionary<byte, object>> SendError { get; protected set; }
        #endregion

        public World(int worldID, string worldName)
        {
            WorldID = worldID;
            WorldName = worldName;
            entityDictionary = new Dictionary<int, Entity>();
            sceneDictionary = new Dictionary<int, Scene>();
            WorldEventManager = new WorldEventManager(this);
            WorldOperationManager = new WorldOperationManager(this);
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
            if (!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
            }
            if (sceneDictionary.ContainsKey(entity.LocatedSceneID))
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
