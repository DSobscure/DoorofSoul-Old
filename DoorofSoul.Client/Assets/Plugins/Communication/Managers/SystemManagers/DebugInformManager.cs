using System;

namespace DoorofSoul.Client.Managers
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
                throw new NullReferenceException();
            }
        }
    }
}

