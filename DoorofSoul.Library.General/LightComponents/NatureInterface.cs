using DoorofSoul.Library.General.LightComponents.Nature;
using DoorofSoul.Library.General.NatureComponents;

namespace DoorofSoul.Library.General.LightComponents
{
    public interface NatureInterface
    {
        ContainerElementsInterface ContainerElementsInterface { get; }
        SceneElementsInterface SceneElementsInterface { get; }

        Container FindContainer(int containerID);
    }
}
