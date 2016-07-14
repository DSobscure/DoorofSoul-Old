using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;
using DoorofSoul.Server.DatabaseElements.Repositories;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLPlayerRepository : PlayerRepository
    {
        public override bool Contains(string account)
        {
            throw new NotImplementedException();
        }

        public override Player Find(string account)
        {
            throw new NotImplementedException();
        }

        public override void Save(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
