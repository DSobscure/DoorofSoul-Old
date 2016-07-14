using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorofSoul.Server.DatabaseElements.Authentications
{
    public abstract class PlayerAuthentication
    {
        public abstract bool LoginCheck(string account, string password, string answerOfLife);
    }
}
