using System;
using System.Collections.Generic;
using DoorofSoul.Library.General.SoulElements;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Soul;

namespace DoorofSoul.Library.General.Operations.Handlers.Soul.FetchData
{
    internal class FetchSkillInfosHandler : FetchDataHandler
    {
        internal FetchSkillInfosHandler(General.Soul soul) : base(soul)
        {
        }

        internal override bool CheckParameter(Dictionary<byte, object> parameter, out string debugMessage)
        {
            if (parameter.Count != 0)
            {
                debugMessage = string.Format("Soul Fetch SkillInfos Parameter Error Parameter Count: {0}", parameter.Count);
                return false;
            }
            else
            {
                debugMessage = null;
                return true;
            }
        }

        internal override bool Handle(SoulFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (SkillInfo info in soul.SkillLibrary.SkillInfos)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchSkillInfosResponseParameterCode.SkillInfo, info }
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Soul Fetch SkillInfos Invalid Cast!");
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.Error(ex.Message);
                    LibraryInstance.Error(ex.StackTrace);
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
