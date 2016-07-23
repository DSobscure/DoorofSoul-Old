using System;
using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Library.General;

namespace DoorofSoul.Client.Scene
{
    public class ScenesManager
    {
        protected Dictionary<int, string> sceneIDNameTable;
        protected Action<string> loadSceneWithNameFunction;

        private int mainSceneID;
        public int MainSceneID { get { return mainSceneID; } }
        private ClientScene mainScene;
        public ClientScene MainScene { get { return mainScene; } }

        protected Action<Entity> instantiateEntityFunction;
        protected Action<Entity> destroyEntityFunction;


        public ScenesManager()
        {
            sceneIDNameTable = new Dictionary<int, string>
            {
                { 1, "TestScene" }
            };
        }
        public void BindFunctions(Action<string> loadSceneWithNameFunction, Action<Entity> instantiateEntityFunction, Action<Entity> destroyEntityFunction)
        {
            this.loadSceneWithNameFunction = loadSceneWithNameFunction;
            this.instantiateEntityFunction = instantiateEntityFunction;
            this.destroyEntityFunction = destroyEntityFunction;
        }

        public void EnterScene(int sceneID)
        {
            if(sceneIDNameTable.ContainsKey(sceneID))
            {
                mainSceneID = sceneID;
                Global.OperationManagers.FetchDataOperationManager.FetchScene(sceneID);
                loadSceneWithNameFunction(sceneIDNameTable[sceneID]);
            }
        }
        public void LoadScene(ClientScene scene)
        {
            if(scene.SceneID == mainSceneID)
            {
                mainScene = scene;
                mainScene.OnEntityEnter += instantiateEntityFunction;
                mainScene.OnEntityExit += destroyEntityFunction;
                Global.OperationManagers.FetchDataOperationManager.FetchSceneEntitiesInformation(mainSceneID);
            }
        }

        public void EntityEnter(Entity entity)
        {
            if(entity.LocatedSceneID == MainSceneID)
            {
                MainScene.EntityEnter(entity);
            }
        }
        public void EntityExit(int entityID, int locatedSceneID)
        {
            if(locatedSceneID == MainSceneID)
            {
                MainScene.EntityExit(entityID);
            }
        }
    }
}
