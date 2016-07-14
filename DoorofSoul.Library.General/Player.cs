using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Library.General
{
    public class Player
    {
        public SupportLauguages UsingLanguage { get; protected set; }
        public Answer Answer { get; protected set; }

        public Player()
        {
            UsingLanguage = SupportLauguages.Chinese_Traditional;
        }
    }
}
