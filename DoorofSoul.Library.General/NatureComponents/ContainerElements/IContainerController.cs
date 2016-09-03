using UnityEngine;

namespace DoorofSoul.Library.General.NatureComponents.ContainerElements
{
    public interface IContainerController
    {
        GameObject GameObject { get; }
        Container Container { get; }
        void BindContainer(Container container);
        void ShowLifePointDelta(decimal delta);
    }
}
