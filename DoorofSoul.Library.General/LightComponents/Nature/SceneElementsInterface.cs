using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.NatureComponents.SceneElements;

namespace DoorofSoul.Library.General.LightComponents.Nature
{
    public interface SceneElementsInterface
    {
        ItemEntity CreateItemEntity(int itemID, int locatedSceneID, DSVector3 position);
        void DeleteItemEntity(int itemEntityID);
    }
}
