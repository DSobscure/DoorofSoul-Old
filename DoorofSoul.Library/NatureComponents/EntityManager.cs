using DoorofSoul.Library.General.NatureComponents;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.NatureComponents
{
    public class EntityManager
    {
        private Dictionary<int, Entity> entityDictionary;
        public IEnumerable<Entity> Entities { get { return entityDictionary.Values; } }

        public EntityManager()
        {
            entityDictionary = new Dictionary<int, Entity>();
        }
        public void Initial()
        {

        }

        public bool ContainsEntity(int entityID)
        {
            return entityDictionary.ContainsKey(entityID);
        }
        public Entity FindEntity(int entityID)
        {
            if (ContainsEntity(entityID))
            {
                return entityDictionary[entityID];
            }
            else
            {
                return null;
            }
        }

        public void ProjectEntity(Entity entity)
        {
            if (!entityDictionary.ContainsKey(entity.EntityID))
            {
                entityDictionary.Add(entity.EntityID, entity);
                if (Hexagram.Nature.SceneManager.ContainsScene(entity.LocatedSceneID))
                {
                    Scene scene = Hexagram.Nature.SceneManager.FindScene(entity.LocatedSceneID);
                    if (Hexagram.Nature.WorldManager.ContainsWorld(scene.WorldID))
                    {
                        Hexagram.Nature.WorldManager.FindWorld(scene.WorldID).EntityEnter(entity);
                        scene.EntityEnter(entity);
                    }
                    else
                    {
                        Hexagram.Log.ErrorFormat("Hexagram: World Not Exist WorldID: {0}", scene.WorldID);
                    }
                }
                else
                {
                    Hexagram.Log.ErrorFormat("Hexagram: Scene Not Exist SceneID: {0}", entity.LocatedSceneID);
                }
            }
        }
        public void ExtractEntity(Entity entity)
        {
            if (entityDictionary.ContainsKey(entity.EntityID))
            {
                if (entity.LocatedScene != null && Hexagram.Nature.WorldManager.ContainsWorld(entity.LocatedScene.WorldID))
                {
                    Database.Database.RepositoryList.NatureRepositoryList.EntityRepository.Save(entity);
                    Scene scene = entity.LocatedScene;
                    scene.EntityExit(entity.EntityID);
                    scene.World.EntityExit(entity);
                }
                entityDictionary.Remove(entity.EntityID);
            }
        }
    }
}
