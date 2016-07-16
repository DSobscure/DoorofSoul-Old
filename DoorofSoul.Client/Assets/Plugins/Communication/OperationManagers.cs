using DoorofSoul.Client.Communication.Managers.OperationManagers;

namespace DoorofSoul.Client.Communication
{
    public class OperationManagers
    {
        public readonly OperationManager OperationManager = new OperationManager();
        public readonly FetchDataOperationManager FetchDataOperationManager = new FetchDataOperationManager();
    }
}

