using System;

namespace DoorofSoul.Client.Communication.Managers.SystemManagers
{
    public class SystemInformManager
    {
        #region Connect Change
        private event Action<bool> onConnectChange;
        public void RegisterConnectChangeFunction(Action<bool> changeFunction)
        {
            onConnectChange += changeFunction;
        }
        public void EraseConnectChangeFunction(Action<bool> changeFunction)
        {
            onConnectChange -= changeFunction;
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
        public void RegisterErrorInformFunction(Action<string> informFunction)
        {
            onErrorInform += informFunction;
        }
        public void EraseErrorInformFunction(Action<string> informFunction)
        {
            onErrorInform -= informFunction;
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

