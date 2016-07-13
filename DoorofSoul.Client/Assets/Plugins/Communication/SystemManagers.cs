using DoorofSoul.Client.Communication.Managers.SystemManagers;

namespace DoorofSoul.Client.Communication
{
    public class SystemManagers
    {
        public readonly SystemInformManager SystemInformManager = new SystemInformManager();
        public readonly DebugInformManager DebugInformManager = new DebugInformManager();
    }
}

