using DoorofSoul.Hexagram.ElementComponents;

namespace DoorofSoul.Hexagram
{
    public class Element
    {
        public ItemManager ItemManager { get; protected set; }

        public Element()
        {
            ItemManager = new ItemManager();
        }
        public void Initial()
        {
            ItemManager.Initial();
        }
    }
}
