using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Soul
{
    internal class SkillOperationResponseHandler : SoulResponseHandler
    {
        internal SkillOperationResponseHandler(ThroneComponents.Soul soul) : base(soul)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 0)
                        {
                            LibraryInstance.ErrorFormat(string.Format("SkillOperationResponse Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                case ErrorCode.NotExist:
                    {
                        LibraryInstance.ErrorFormat("SkillOperationResponse Error DebugMessage: {0}", debugMessage);
                        return false;
                    }
                case ErrorCode.InvalidOperation:
                    {
                        LibraryInstance.ErrorFormat("SkillOperationResponse Error DebugMessage: {0}", debugMessage);
                        return false;
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("SkillOperationResponse Error DebugMessage: {0}", debugMessage);
                        return false;
                    }
            }
        }

        internal override bool Handle(SoulOperationCode operationCode, ErrorCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            return base.Handle(operationCode, returnCode, debugMessage, parameters);
        }
    }
}
