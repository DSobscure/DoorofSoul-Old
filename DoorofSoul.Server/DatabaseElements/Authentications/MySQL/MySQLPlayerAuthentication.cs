using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Server.DatabaseElements.Authentications;

namespace DoorofSoul.Server.DatabaseElements.Authentications.MySQL
{
    public class MySQLPlayerAuthentication : PlayerAuthentication
    {
        public override bool LoginCheck(string account, string password, string answerOfLife)
        {
            throw new NotImplementedException();
        }
    }
}
