using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLContainerRepository : ContainerRepository
    {
        public override Container Find(int containerID)
        {
            throw new NotImplementedException();
        }

        public override List<Container> List()
        {
            throw new NotImplementedException();
        }

        public override List<Container> ListOfSoul(int soulID)
        {
            throw new NotImplementedException();
        }

        public override void Save(Container container)
        {
            throw new NotImplementedException();
        }
    }
}
