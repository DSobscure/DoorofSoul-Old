using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Answer;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Communications.Operations.Handlers.Answer.FetchData
{
    internal class FetchSoulsHandler : FetchDataHandler
    {
        internal FetchSoulsHandler(ThroneComponents.Answer answer) : base(answer, 0)
        {
        }

        internal override bool Handle(AnswerFetchDataCode fetchCode, Dictionary<byte, object> parameter)
        {
            if (base.Handle(fetchCode, parameter))
            {
                try
                {
                    foreach (ThroneComponents.Soul soul in answer.Souls)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchSoulsResponseParameterCode.SoulID, soul.SoulID },
                            { (byte)FetchSoulsResponseParameterCode.SoulName, soul.SoulName },
                            { (byte)FetchSoulsResponseParameterCode.SoulAttributes, soul.Attributes }
                        };
                        SendResponse(fetchCode, result);
                    }
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Fetch Souls Invalid Cast!");
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
