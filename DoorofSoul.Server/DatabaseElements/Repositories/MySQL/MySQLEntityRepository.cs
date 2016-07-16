using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Library.General;

namespace DoorofSoul.Server.DatabaseElements.Repositories.MySQL
{
    public class MySQLEntityRepository : EntityRepository
    {
        public override Entity Find(int entityID)
        {
            throw new NotImplementedException();
        }

        public override List<Entity> List()
        {
            throw new NotImplementedException();
        }

        public override List<Entity> ListInScene(int sceneID)
        {
            throw new NotImplementedException();
        }

        public override void Save(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
