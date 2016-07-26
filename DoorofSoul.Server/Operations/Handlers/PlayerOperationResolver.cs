using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class PlayerOperationResolver : OperationHandler
    {
        public PlayerOperationResolver(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("Player Operation Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(OperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)OperationParameterCode.OperationCode];
                    int playerID = (int)parameters[(byte)OperationParameterCode.ID];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)OperationParameterCode.Parameters];
                    if(playerID != peer.Player.PlayerID)
                    {
                        peer.Player.PlayerOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        Application.Log.ErrorFormat("PlayerOperation Error PlayerID: {0} Not In Peer", playerID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("PlayerOperation Parameter Cast Error");
                    Application.Log.ErrorFormat(ex.Message);
                    Application.Log.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Application.Log.ErrorFormat(ex.Message);
                    Application.Log.ErrorFormat(ex.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
