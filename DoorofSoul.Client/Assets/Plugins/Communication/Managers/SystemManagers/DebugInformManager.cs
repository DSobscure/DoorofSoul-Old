using System;

namespace DoorofSoul.Client.Communication.Managers.SystemManagers
{
    public class DebugInformManager
    {
        private event Action<string> onDebugInform;
        public void RegisterDebugInformFunction(Action<string> informFunction)
        {
            onDebugInform += informFunction;
        }
        public void EraseDebugInformFunction(Action<string> informFunction)
        {
            onDebugInform -= informFunction;
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

