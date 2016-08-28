using System;
using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.LightComponents.HexagramInterfaces.Nature;
using DoorofSoul.Library.General.NatureComponents.SceneElements;

namespace DoorofSoul.Hexagram.LightComponents.Nature
{
    class HexagramSceneElementsInterface : SceneElementsInterface
    {
        public ItemEntity CreateItemEntity(int itemID, int locatedSceneID, DSVector3 position)
        {
            return Database.Database.RepositoryList.NatureRepositoryList.SceneElementsRepositoryList.ItemEntityRepository.Create(itemID, locatedSceneID, position);
        }

        public void DeleteItemEntity(int itemEntityID)
        {
            Database.Database.RepositoryList.NatureRepositoryList.SceneElementsRepositoryList.ItemEntityRepository.Delete(itemEntityID);
        }
    }
}
