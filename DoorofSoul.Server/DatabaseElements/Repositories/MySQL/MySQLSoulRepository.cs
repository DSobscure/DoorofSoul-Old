using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLSoulRepository : SoulRepository
    {
        public override Soul Find(int soulID)
        {
            throw new NotImplementedException();
        }

        public override List<Soul> List()
        {
            throw new NotImplementedException();
        }

        public override List<Soul> ListOfAnswer(int answerID)
        {
            throw new NotImplementedException();
        }

        public override List<Soul> ListOfContainer(int containerID)
        {
            throw new NotImplementedException();
        }

        public override void Save(Soul soul)
        {
            throw new NotImplementedException();
        }
    }
}
