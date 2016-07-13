using DoorofSoul.Client.Communication.Managers;
using DoorofSoul.Client.Communication.Managers.EventManagers;

namespace DoorofSoul.Client.Communication
{
    public class EventManagers
    {
        public readonly InformDataEventManager InformDataEventManager = new InformDataEventManager();
        public readonly EventManager EventManager = new EventManager();
    }
}
