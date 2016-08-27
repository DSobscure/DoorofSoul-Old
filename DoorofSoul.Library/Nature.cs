using DoorofSoul.Hexagram.NatureComponents;

namespace DoorofSoul.Hexagram
{
    public class Nature
    {
        public WorldManager WorldManager { get; protected set; }
        public SceneManager SceneManager { get; protected set; }
        public ContainerManager ContainerManager { get; protected set; }
        public EntityManager EntityManager { get; protected set; }

        public Nature()
        {
            WorldManager = new WorldManager();
            SceneManager = new SceneManager();
            ContainerManager = new ContainerManager();
            EntityManager = new EntityManager();
        }
        public void Initial()
        {
            WorldManager.Initial();
            SceneManager.Initial();
            ContainerManager.Initial();
            EntityManager.Initial();
        }
    }
}
