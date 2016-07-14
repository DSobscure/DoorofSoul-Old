using System;

namespace DoorofSoul.Client.Communication.Managers.SystemManagers
{
    public class DebugInformManager
    {
        private event Action<string> onDebugInform;
        public event Action<string> OnDebugInform
        {
            add{ onDebugInform += value; }
            remove { onDebugInform -= value; }
        }
        public void DebugInform(string message)
        {
            if(onDebugInform != null)
            {
                onDebugInform(message);
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform("DebugInform Event is null");
            }
        }
    }
}

