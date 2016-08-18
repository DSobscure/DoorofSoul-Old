using System;
using System.Collections.Generic;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.OperationCodes;
using DoorofSoul.Protocol.Communication.OperationParameters.Soul;

namespace DoorofSoul.Library.General.Operations.Handlers.Soul
{
    internal class SkillOperationHandler : SoulOperationHandler
    {
        internal SkillOperationHandler(General.Soul soul) : base(soul, 4)
        {
        }

        internal override bool Handle(SoulOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                try
                {
                    int agentContainerID = (int)parameters[(byte)SkillOperationParameterCode.AgentContainerID];
                    HeptagramSystemTypeCode heptagramSystem = (HeptagramSystemTypeCode)parameters[(byte)SkillOperationParameterCode.HeptagramSystem];
                    int skillInfoID = (int)parameters[(byte)SkillOperationParameterCode.SkillInfoID];
                    Dictionary<byte, object> skillParameters = (Dictionary<byte, object>)parameters[(byte)SkillOperationParameterCode.SkillParameters];
                    if(soul.ContainsContainer(agentContainerID))
                    {
                        if (soul.SkillLibrary.ContainsSkillInfo(heptagramSystem, skillInfoID))
                        {
                            Dictionary<byte, object> skillResponseParameters;
                            ErrorCode errorCode;
                            string debugMessage;
                            if (soul.SkillKnowledgeInterface.OperateSkill(soul, soul.FindContainer(agentContainerID), soul.SkillLibrary.FindSkillInfo(heptagramSystem, skillInfoID), skillParameters, out skillResponseParameters, out errorCode, out debugMessage))
                            {
                                SendResponse(operationCode, skillResponseParameters);
                                return true;
                            }
                            else
                            {
                                SendError(operationCode, errorCode, debugMessage);
                                return false;
                            }
                        }
                        else
                        {
                            LibraryInstance.ErrorFormat("Soul SkillOperation Error Soul ID: {0} Dosen't Have SkillInfo System: {1} ID: {2}", soul.SoulID, heptagramSystem, skillInfoID);
                            SendError(operationCode, ErrorCode.NotExist, "Dosen't Have This SkillInfo");
                            return false;
                        }
                    }
                    else
                    {
                        LibraryInstance.ErrorFormat("Soul SkillOperation Error Soul ID: {0} Dosen't Have Agent ContainerID: {1}", soul.SoulID, agentContainerID);
                        SendError(operationCode, ErrorCode.NotExist, "Dosen't Have This Agent");
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    LibraryInstance.ErrorFormat("Soul SkillOperation Parameter Cast Error");
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    LibraryInstance.ErrorFormat(ex.Message);
                    LibraryInstance.ErrorFormat(ex.StackTrace);
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
