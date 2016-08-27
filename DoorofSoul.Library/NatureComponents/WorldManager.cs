using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.NatureComponents
{
    public class WorldManager
    {
        private Dictionary<int, World> worldDictionary;
        public IEnumerable<World> Worlds { get { return worldDictionary.Values; } }

        public WorldManager()
        {
            worldDictionary = new Dictionary<int, World>();
        }
        public void Initial()
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
                foreach (Scene scene in sceneList)
                {
                    Hexagram.Nature.SceneManager.LoadScene(scene);
                }
            }
        }

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
    }
}
