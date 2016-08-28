using DoorofSoul.Library.General.LightComponents.HexagramInterfaces;

namespace DoorofSoul.Hexagram.LightComponents
{
    public class HexagramInterface : LibraryHexagramInterface
    {
        private HexagramKnowledgeInterface knowledgeInterface;
        private HexagramElementInterface elementInterface;
        private HexagramNatureInterface natureInterface;

        public KnowledgeInterface KnowledgeInterface { get { return knowledgeInterface; } }
        public ElementInterface ElementInterface { get { return elementInterface; } }
        public NatureInterface NatureInterface { get { return natureInterface; } }

        public HexagramInterface()
        {
            knowledgeInterface = new HexagramKnowledgeInterface();
            elementInterface = new HexagramElementInterface();
            natureInterface = new HexagramNatureInterface();
        }
    }
}
