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

        public bool ClientVersionCheck()
        {
            if(CurrentClientVersion != LocalClientVersion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region current server version change
        private event Action<string> onCurrentServerVersionChange;
        public event Action<string> OnCurrentServerVersionChange { add { onCurrentServerVersionChange += value; } remove { onCurrentServerVersionChange -= value; } }
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
        public event Action<string> OnCurrentClientVersionChange { add { onCurrentClientVersionChange += value; } remove { onCurrentClientVersionChange -= value; } }
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
