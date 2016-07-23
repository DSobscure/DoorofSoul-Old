using DoorofSoul.Library;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication.OperationParameters;
using DoorofSoul.Protocol.Communication.ResponseParameters;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Linq;

namespace DoorofSoul.Server.Operations.Handlers
{
    public class ActivateSoulHandler : OperationHandler
    {
        public ActivateSoulHandler(Peer peer) : base(peer)
        {
        }

        public override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 1)
            {
                debugMessage = "Activate Soul Operation Parameter Error";
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        public override bool Handle(OperationRequest operationRequest)
        {
            if (base.Handle(operationRequest))
            {
                string debugMessage, errorMessage;
                int soulID = (int)operationRequest.Parameters[(byte)ActivateSoulOperationParameterCode.SoulID];
                if (peer.Player.Answer.ContainsSoul(soulID))
                {
                    if (Hexagram.Instance.Throne.ActiveSoul(soulID))
                    {
                        Soul soul = peer.Player.Answer.FindSoul(soulID);
                        Container defaultContainer = soul.Containers.FirstOrDefault();
                        int sceneID;
                        if(defaultContainer == null)
                        {
                            sceneID = 1;
                        }
                        else
                        {
                            sceneID = defaultContainer.LocatedSceneID;
                        }
                        Dictionary<byte, object> parameters = new Dictionary<byte, object>
                        {
                            { (byte)ActiveSoulResponseParameterCode.SoulID, soulID },
                            { (byte)ActiveSoulResponseParameterCode.SceneID, sceneID }
                        };
                        SendResponse(operationRequest.OperationCode, parameters);
                        return true;
                    }
                    else
                    {
                        debugMessage = string.Format("Soul Activate Error SoulID: {0}, AnswerID: {1}", soulID, peer.Player.Answer.AnswerID);
                        errorMessage = LauguageDictionarySelector.Instance[peer.UsingLanguage]["Activate Soul Error"];
                        SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.Fail, debugMessage, errorMessage);
                        return false;
                    }
                }
                else
                {
                    debugMessage = string.Format("Soul Activate Permission Deny SoulID: {0}, AnswerID: {1}", soulID, peer.Player.Answer.AnswerID);
                    errorMessage = LauguageDictionarySelector.Instance[peer.UsingLanguage]["Permission Deny"];
                    SendError(operationRequest.OperationCode, Protocol.Communication.ErrorCode.PermissionDeny, debugMessage, errorMessage);
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
