using ExitGames.Logging;

namespace DoorofSoul.Hexagram
{
    public class Hexagram
    {
        private static Hexagram instance;

        static Hexagram()
        {
            instance = new Hexagram();
        }
        public static void Initial(ILogger log)
        {
            Log = log;
            Knowledge.Initial();
            Element.Initial();
            Nature.Initial();
            Mind.Initial();
            Throne.Initial();
        }

        public static Knowledge Knowledge { get; protected set; }
        public static Element Element { get; protected set; }
        public static Nature Nature { get; protected set; }
        public static Mind Mind { get; protected set; }
        public static Throne Throne { get; protected set; }
        public static ILogger Log { get; protected set; }

        
        protected Hexagram()
        {
            Knowledge = new Knowledge();
            Element = new Element();
            Nature = new Nature();
            Mind = new Mind();
            Throne = new Throne();
        }
    }
}
