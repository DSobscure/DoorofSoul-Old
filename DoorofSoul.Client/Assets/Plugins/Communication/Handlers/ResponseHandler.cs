using ExitGames.Client.Photon;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Client.Handlers
{
    public abstract class ResponseHandler
    {
        public virtual bool Handle(OperationResponse operationResponse)
        {
            if (CheckError(operationResponse))
            {
                return true;
            }
            else
            {
                Global.SystemManagers.DebugInformManager.DebugInform(string.Format("Response Error On {0}", (OperationCode)operationResponse.OperationCode));
                return false;
            }
        }
        public abstract bool CheckError(OperationResponse operationResponse);
    }
}

