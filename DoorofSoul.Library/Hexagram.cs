using ExitGames.Logging;

namespace DoorofSoul.Hexagram
{
    public class Hexagram
    {
        private static Hexagram instance;
        public static Hexagram Instance { get { return instance; } }

        static Hexagram()
        {
            instance = new Hexagram();
        }
        public static void Initial(ILogger log)
        {
            instance.Log = log;
        }

        public Knowledge Knowledge { get; protected set; }
        public Element Element { get; protected set; }
        public Nature Nature { get; protected set; }
        public Throne Throne { get; protected set; }
        public ILogger Log { get; protected set; }

        
        protected Hexagram()
        {
            Knowledge = new Knowledge();
            Element = new Element();
            Nature = new Nature();
            Throne = new Throne();
        }
    }
}
