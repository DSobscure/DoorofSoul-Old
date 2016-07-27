using System;
using UnityEngine;

namespace DoorofSoul.Client.Global
{
    public class SystemManager
    {
        public static void ErrorFormat(string message, params object[] parameters)
        {
            Debug.Log(string.Format(message, parameters));
        }
        public static void Error(object message)
        {
            Debug.Log(message);
        }

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
                Error("ConnectChange Event is null");
            }
        }
        #endregion
    }
}
