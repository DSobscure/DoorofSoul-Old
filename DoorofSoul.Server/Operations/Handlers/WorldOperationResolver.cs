using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class WorldOperationResolver : OperationHandler
    {
        public WorldOperationResolver(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 3)
            {
                debugMessage = string.Format("World Operation Parameter Error Parameter Count: {0}", parameter.Count);
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
                    int worldID = (int)parameters[(byte)OperationParameterCode.ID];
                    WorldOperationCode resolvedOperationCode = (WorldOperationCode)parameters[(byte)OperationParameterCode.OperationCode];
                    Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)OperationParameterCode.Parameters];
                    if (Hexagram.Hexagram.Nature.WorldManager.ContainsWorld(worldID))
                    {
                        Hexagram.Hexagram.Nature.WorldManager.FindWorld(worldID).WorldOperationManager.Operate(resolvedOperationCode, resolvedParameters);
                        return true;
                    }
                    else
                    {
                        Application.Log.ErrorFormat("WorldOperation Error WorldID: {0} Not In Hexagram", worldID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Application.Log.ErrorFormat("WorldOperation Parameter Cast Error");
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
