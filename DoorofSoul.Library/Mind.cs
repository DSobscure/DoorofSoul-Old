using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Hexagram.MindComponents;

namespace DoorofSoul.Hexagram
{
    public class Mind
    {
        public SoulManager SoulManager { get; protected set; }

        public Mind()
        {
            SoulManager = new SoulManager();
        }
        public void Initial()
        {
            SoulManager.Initial();
        }
    }
}
