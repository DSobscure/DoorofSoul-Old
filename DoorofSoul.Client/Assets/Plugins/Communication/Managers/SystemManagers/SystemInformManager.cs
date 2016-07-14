using System;

namespace DoorofSoul.Client.Communication.Managers.SystemManagers
{
    public class SystemInformManager
    {
        #region Connect Change
        private event Action<bool> onConnectChange;
        public event Action<bool> OnConnectChange
        {
            add { onConnectChange += value; }
            remove { onConnectChange -= value; }
        }
        public void ConnectChange(bool connectStatus)
        {
            if (onConnectChange != null)
            {
                onConnectChange(connectStatus);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("ConnectChange Event is null");
            }
        }
        #endregion

        #region Error Inform
        private event Action<string> onErrorInform;
        public event Action<string> OnErrorInform
        {
            add { onErrorInform += value; }
            remove { onErrorInform -= value; }
        }
        public void ErrorInform(string errorMessage)
        {
            if (onErrorInform != null)
            {
                onErrorInform(errorMessage);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("ErrorInform Event is null");
            }
        }
        #endregion
    }
}

