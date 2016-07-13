using DoorofSoul.Client.Communication.Managers;
using DoorofSoul.Client.Communication.Managers.EventManagers;

namespace DoorofSoul.Client.Communication
{
    public class EventManagers
    {
        public readonly EventManager EventManager = new EventManager();
        public readonly InformDataEventManager InformDataEventManager = new InformDataEventManager();
    }
}
