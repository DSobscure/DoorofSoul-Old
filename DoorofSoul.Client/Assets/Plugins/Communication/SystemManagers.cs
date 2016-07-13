using DoorofSoul.Client.Communication.Managers.SystemManagers;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Client.Communication
{
    public class SystemManagers
    {
        public readonly SystemInformManager SystemInformManager = new SystemInformManager();
        public readonly DebugInformManager DebugInformManager = new DebugInformManager();
        public SupportLauguages UsingLauguage { get; set; }
    }
}

