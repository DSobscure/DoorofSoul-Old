using DoorofSoul.Client.Communication.Managers.ResponseManagers;

namespace DoorofSoul.Client.Communication
{
    public class ResponseManagers
    {
        public readonly ResponseManager ResponseManager = new ResponseManager();
        public readonly UIResponseManager UIResponseManager = new UIResponseManager();
    }
}

