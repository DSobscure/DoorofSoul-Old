﻿using DoorofSoul.Library.General;
using System.Collections.Generic;
using ExitGames.Logging;

namespace DoorofSoul.Library
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

        public Nature Nature { get; protected set; }
        public Throne Throne { get; protected set; }
        public ILogger Log { get; protected set; }

        
        protected Hexagram()
        {
            Nature = new Nature();
            Throne = new Throne();
        }
    }
}
