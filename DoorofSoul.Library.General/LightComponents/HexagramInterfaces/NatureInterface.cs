using DoorofSoul.Library.General.LightComponents.HexagramInterfaces.Nature;
using DoorofSoul.Library.General.NatureComponents;

namespace DoorofSoul.Library.General.LightComponents.HexagramInterfaces
{
    public interface NatureInterface
    {
        ContainerElementsInterface ContainerElementsInterface { get; }
        SceneElementsInterface SceneElementsInterface { get; }

        Container FindContainer(int containerID);
    }
}
