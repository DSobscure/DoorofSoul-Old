using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorofSoul.Hexagram.NatureComponents
{
    public class SceneManager
    {
        private Dictionary<int, Scene> sceneDictionary;
        public IEnumerable<Scene> Scenes { get { return sceneDictionary.Values; } }

        public Scene FirstScene { get { return Scenes.First(); } }

        public SceneManager()
        {
            sceneDictionary = new Dictionary<int, Scene>();
        }
        public void Initial()
        {

        }

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
        public void LoadScene(Scene scene)
        {
            if(!ContainsScene(scene.SceneID))
            {
                sceneDictionary.Add(scene.SceneID, scene);
                AssemblyScene(scene);
            }
        }
        public void AssemblyScene(Scene scene)
        {
            scene.SetSceneEye(new ProvidenceEye());
            scene.ItemEntityManager.InitialItemEntities(Database.Database.RepositoryList.NatureRepositoryList.SceneElementsRepositoryList.ItemEntityRepository.ListOfScene(scene.SceneID));

            scene.OnEntityEnter += scene.SceneEventManager.InformDataResolver.InformEntityEnter;
            scene.OnEntityExit += scene.SceneEventManager.InformDataResolver.InformEntityExit;
            scene.OnContainerEnter += scene.SceneEventManager.InformDataResolver.InformContainerEnter;
            scene.OnContainerEnter += (container) => 
            {
                container.Attributes.OnLifePointChange += (lifePoint) => scene.SceneEventManager.InformDataResolver.InformContainerLifePointChange(container.ContainerID, lifePoint);
            };
            scene.OnContainerExit += scene.SceneEventManager.InformDataResolver.InformContainerExit;
            scene.ItemEntityManager.OnItemEntityChange += scene.SceneEventManager.InformDataResolver.InformItemEntityChange;

            scene.BulletManager.OnShootABullet += scene.SceneEventManager.InformDataResolver.InformShootABullet;
            scene.BulletManager.OnDestroyBullet += scene.SceneEventManager.InformDataResolver.InformDestroyBullet;
            scene.BulletManager.OnShootABullet += async(bullet) =>
            {
                await Task.Delay(2000);
                scene.BulletManager.RemoveBullet(bullet.BulletID);
            };
        }
    }
}
