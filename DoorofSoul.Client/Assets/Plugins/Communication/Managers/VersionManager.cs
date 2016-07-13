using System;

namespace DoorofSoul.Client.Communication.Managers
{
    public class VersionManager
    {
        private string currentServerVersion;
        public string CurrentServerVersion
        {
            get { return currentServerVersion; }
            set
            {
                currentServerVersion = value;
                CurrentServerVersionChange(currentServerVersion);
            }
        }

        private string currentClientVersion;
        public string CurrentClientVersion
        {
            get { return currentClientVersion; }
            set
            {
                currentClientVersion = value;
                CurrentClientVersionChange(currentClientVersion);
            }
        }
        public string LocalClientVersion { get; private set; }

        public VersionManager()
        {
            LocalClientVersion = "Test 0.0.1";
        }

        #region current server version change
        private event Action<string> onCurrentServerVersionChange;
        public void RegisterCurrentServerVersionChangeFunction(Action<string> changeFunction)
        {
            onCurrentServerVersionChange += changeFunction;
        }
        public void EraseCurrentServerVersionChangeFunction(Action<string> changeFunction)
        {
            onCurrentServerVersionChange -= changeFunction;
        }
        private void CurrentServerVersionChange(string version)
        {
            if(onCurrentServerVersionChange != null)
            {
                onCurrentServerVersionChange(version);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("CurrentServerVersionChange Event is null");
            }
        }
        #endregion

        #region current client version change
        private event Action<string> onCurrentClientVersionChange;
        public void RegisterCurrentClientVersionChangeFunction(Action<string> changeFunction)
        {
            onCurrentClientVersionChange += changeFunction;
        }
        public void EraseCurrentClientVersionChangeFunction(Action<string> changeFunction)
        {
            onCurrentClientVersionChange -= changeFunction;
        }
        private void CurrentClientVersionChange(string version)
        {
            if (onCurrentClientVersionChange != null)
            {
                onCurrentClientVersionChange(version);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("CurrentClientVersionChange Event is null");
            }
        }
        #endregion
    }
}
