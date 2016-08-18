using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Language;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.FetchDataCodes;
using DoorofSoul.Protocol.Communication.FetchDataResponseParameters.Soul;
using DoorofSoul.Library.General.KnowledgeComponents.Skill;

namespace DoorofSoul.Library.General.LightComponents.Communications.Responses.Handlers.Soul.FetchData
{
    internal class FetchSkillInfosResponseHandler : FetchDataResponseHandler
    {
        internal FetchSkillInfosResponseHandler(ThroneComponents.Soul soul) : base(soul)
        {
        }

        internal override bool CheckError(Dictionary<byte, object> parameters, ErrorCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ErrorCode.NoError:
                    {
                        if (parameters.Count != 1)
                        {
                            LibraryInstance.ErrorFormat(string.Format("Fetch SoulContainerLinks Response Parameter Error, Parameter Count: {0}", parameters.Count));
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                default:
                    {
                        LibraryInstance.ErrorFormat("Fetch SkillInfos Response Error DebugMessage: {0}", debugMessage);
                        soul.SoulEventManager.ErrorInform(LauguageDictionarySelector.Instance[soul.UsingLanguage]["Unknown Error"], LauguageDictionarySelector.Instance[soul.UsingLanguage]["Fetch SkillInfos Error"]);
                        return false;
                    }
            }
        }

        internal override bool Handle(SoulFetchDataCode fetchCode, ErrorCode returnCode, string fetchDebugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(fetchCode, returnCode, fetchDebugMessage, parameters))
            {
                try
                {
                    SkillInfo skillInfo = (SkillInfo)parameters[(byte)FetchSkillInfosResponseParameterCode.SkillInfo];
                    soul.SkillLibrary.LoadSkillInfo(skillInfo);
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.Error("Fetch SkillInfos Parameter Cast Error");
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
