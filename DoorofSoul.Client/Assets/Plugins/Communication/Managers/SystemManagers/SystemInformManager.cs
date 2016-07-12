using System;

namespace DoorofSoul.Client.Managers
{
    public class SystemInformManager
    {
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
                throw new NullReferenceException();
            }
        }
    }
}

