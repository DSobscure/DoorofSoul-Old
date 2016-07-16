using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoorofSoul.Client.Communication.Managers.ResponseManagers
{
    public class UIResponseManager
    {
        #region player login result
        private event Action onPlayerLoginResult;
        public event Action OnPlayerLoginResult
        {
            add { onPlayerLoginResult += value; }
            remove { onPlayerLoginResult -= value; }
        }
        public void PlayerLoginResult()
        {
            if (onPlayerLoginResult != null)
            {
                onPlayerLoginResult();
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("PlayerLoginResult UI Event is null");
            }
        }
        #endregion
    }
}
