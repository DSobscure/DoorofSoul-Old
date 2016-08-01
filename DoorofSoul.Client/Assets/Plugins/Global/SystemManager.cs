using System;
using UnityEngine;
using DoorofSoul.Protocol.Language;

namespace DoorofSoul.Client.Global
{
    public class SystemManager
    {
        private string currentServerVersion;
        public string CurrentServerVersion
        {
            get { return currentServerVersion; }
            set
            {
                currentServerVersion = value;
                if(onCurrentServerVersionChange != null)
                {
                    onCurrentServerVersionChange(value);
                }
                else
                {
                    Error("onCurrentServerVersionChange event is null");
                }
            }
        }

        private string currentClientVersion;
        public string CurrentClientVersion
        {
            get { return currentClientVersion; }
            set
            {
                currentClientVersion = value;
                if (onCurrentClientVersionChange != null)
                {
                    onCurrentClientVersionChange(value);
                }
                else
                {
                    Error("onCurrentClientVersionChange event is null");
                }
            }
        }
        public string LocalClientVersion { get; private set; }
        public static SupportLauguages SystemLanguage { get { return SupportLauguages.Chinese_Traditional; } }

        #region events
        #region OnErrorInform
        private event Action<string, string> onErrorInform;
        public event Action<string, string> OnErrorInform
        {
            add { onErrorInform += value; }
            remove { onErrorInform -= value; }
        }
        #endregion
        #endregion

        public SystemManager()
        {
            LocalClientVersion = "Test 0.0.1";
        }

        private event Action<string> onCurrentServerVersionChange;
        public event Action<string> OnCurrentServerVersionChange { add { onCurrentServerVersionChange += value; } remove { onCurrentServerVersionChange -= value; } }

        private event Action<string> onCurrentClientVersionChange;
        public event Action<string> OnCurrentClientVersionChange { add { onCurrentClientVersionChange += value; } remove { onCurrentClientVersionChange -= value; } }

        public static void ErrorFormat(string message, params object[] parameters)
        {
            Debug.Log(string.Format(message, parameters));
        }
        public static void Error(object message)
        {
            Debug.Log(message);
        }

        public bool ClientVersionCheck()
        {
            if (CurrentClientVersion != LocalClientVersion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ErrorInform(string title, string message)
        {
            if(onErrorInform != null)
            {
                onErrorInform(title, message);
            }
            else
            {
                Error("onErrorInform event is null");
            }
        }
    }
}
