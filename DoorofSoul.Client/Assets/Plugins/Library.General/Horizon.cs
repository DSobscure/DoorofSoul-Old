using System.Collections.Generic;
using DoorofSoul.Library.General;
using DoorofSoul.Client.Global;

namespace DoorofSoul.Client.Library.General
{
    public class Horizon
    {
        protected readonly Dictionary<int, ClientWorld> worldDictionary;
        public IEnumerable<ClientWorld> Worlds { get { return worldDictionary.Values; } }
        public DoorofSoul.Library.General.Scene MainScene { get; protected set; }

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
        public void LoadScene(Scene scene)
        {
            if(ContainsWorld(scene.WorldID))
            {
                FindWorld(scene.WorldID).LoadScenes(new List<Scene> { scene });
            }
            else
            {
                SystemManager.ErrorFormat("Load Scene Error World Not Exist WorldID: {0}, SceneID: {1}", scene.WorldID, scene.SceneID);
            }
        }
    }
}
