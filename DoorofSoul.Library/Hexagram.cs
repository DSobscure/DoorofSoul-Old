using DoorofSoul.Library.General;
using System.Collections.Generic;

namespace DoorofSoul.Library
{
    public class Hexagram
    {
        private static Hexagram instance;
        public static Hexagram Instance { get { return instance; } }

        public Nature Nature { get; protected set; }
        public Throne Throne { get; protected set; }

        static Hexagram()
        {
            instance = new Hexagram();
        }
        protected Hexagram()
        {
            Nature = new Nature();
            Throne = new Throne();
        }
    }
}
