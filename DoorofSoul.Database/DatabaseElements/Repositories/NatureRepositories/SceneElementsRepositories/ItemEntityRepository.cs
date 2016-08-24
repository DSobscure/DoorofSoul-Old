using DoorofSoul.Library.General.NatureComponents.SceneElements;
using System.Collections.Generic;
using DoorofSoul.Library.General.ElementComponents;

namespace DoorofSoul.Database.DatabaseElements.Repositories.NatureRepositories.SceneElementsRepositories
{
    public abstract class ItemEntityRepository
    {
        public abstract ItemEntity Create(int itemID, int locatedSceneID, DSVector3 position);
        public abstract void Delete(int itemEntityID);
        public abstract ItemEntity Find(int itemEntityID);
        public abstract void Save(ItemEntity itemEntity);
        public abstract List<ItemEntity> ListOfScene(int sceneID);
    }
}
