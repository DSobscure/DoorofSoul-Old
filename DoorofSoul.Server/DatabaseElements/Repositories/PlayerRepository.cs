using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server.DatabaseElements.Repositories
{
    public abstract class PlayerRepository
    {
        public abstract bool Contains(string account);
        public abstract Player Find(string account);
        public abstract void Save(Player player);
    }
}
