using System;
using DoorofSoul.Hexagram.LightComponents.Nature;
using DoorofSoul.Library.General.LightComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.LightComponents.Nature;

namespace DoorofSoul.Hexagram.LightComponents
{
    public class HexagramNatureInterface : NatureInterface
    {
        private HexagramContainerElementsInterface containerElementsInterface;
        private HexagramSceneElementsInterface sceneElementsInterface;

        public ContainerElementsInterface ContainerElementsInterface
        {
            get
            {
                return containerElementsInterface;
            }
        }

        public SceneElementsInterface SceneElementsInterface
        {
            get
            {
                return sceneElementsInterface;
            }
        }

        public HexagramNatureInterface()
        {
            containerElementsInterface = new HexagramContainerElementsInterface();
            sceneElementsInterface = new HexagramSceneElementsInterface();
        }

        public Container FindContainer(int containerID)
        {
            return Hexagram.Nature.ContainerManager.FindContainer(containerID);
        }
    }
}
