using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Global;
using UnityEngine.SceneManagement;

namespace DoorofSoul.Client.Library.General
{
    public class Horizon
    {
        protected readonly Dictionary<int, ClientWorld> worldDictionary;
        public IEnumerable<ClientWorld> Worlds { get { return worldDictionary.Values; } }
        public DoorofSoul.Library.General.Scene MainScene { get; protected set; }
        private int waitingMainSceneID;

        public Horizon()
        {
            worldDictionary = new Dictionary<int, ClientWorld>();
        }

        public bool ContainsWorld(int worldID)
        {
            return worldDictionary.ContainsKey(worldID);
        }
        public ClientWorld FindWorld(int worldID)
        {
            if(ContainsWorld(worldID))
            {
                return worldDictionary[worldID];
            }
            else
            {
                return null;
            }
        }
        public void LoadWorld(ClientWorld world)
        {
            if(!ContainsWorld(world.WorldID))
            {
                worldDictionary.Add(world.WorldID, world);
            }
            else
            {
                SystemManager.ErrorFormat("World alreay Exist WorldID: {0}", world.WorldID);
            }
        }
        public void LoadScene(DoorofSoul.Library.General.Scene scene)
        {
            if(ContainsWorld(scene.WorldID))
            {
                FindWorld(scene.WorldID).LoadScenes(new List<DoorofSoul.Library.General.Scene> { scene });
                foreach(Container container in Global.Global.Player.Answer.Containers)
                {
                    if (container.Entity.LocatedSceneID == scene.SceneID)
                    {
                        scene.ContainerEnter(container);
                    }
                }
                if(scene.SceneID == waitingMainSceneID)
                {
                    ChangeToScene(scene);
                }
            }
            else
            {
                SystemManager.ErrorFormat("Load Scene Error World Not Exist WorldID: {0}, SceneID: {1}", scene.WorldID, scene.SceneID);
            }
        }
        public void ChangeToScene(DoorofSoul.Library.General.Scene scene)
        {
            if(ContainsWorld(scene.WorldID))
            {
                if(FindWorld(scene.WorldID).ContainsScene(scene.SceneID))
                {
                    MainScene = scene;
                    SceneManager.LoadScene(scene.SceneName + "Scene");
                }
                else
                {
                    SystemManager.ErrorFormat("Try To Cgange To Not Existed Scene SceneID: {0}", scene.SceneID);
                }
            }
            else
            {
                SystemManager.ErrorFormat("Try To Cgange To Not Existed World WorldID: {0} SceneID: {1}", scene.WorldID, scene.SceneID);
            }
        }
        public void FetchMainScene(int sceneID)
        {
            waitingMainSceneID = sceneID;
            DoorofSoul.Library.General.Scene scene;
            foreach(World world in Worlds)
            {
                world.FetchScene(sceneID, out scene);
            }
        }
    }
}
