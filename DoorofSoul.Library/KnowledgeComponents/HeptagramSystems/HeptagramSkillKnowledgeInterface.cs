using DoorofSoul.Library.General.Skills;
using DoorofSoul.Library.General;
using DoorofSoul.Protocol.Communication;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Library.KnowledgeComponents.HeptagramSystems
{
    public class HeptagramSkillKnowledgeInterface : SkillKnowledgeInterface
    {
        public bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            HeptagramSystem system = Hexagram.Instance.Knowledge.ChoseSystem(skillInfo.Skill.SystemTypeCode);
            if(system != null)
            {
                return system.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage);
            }
            else
            {
                skillResponseParameters = new Dictionary<byte, object>();
                errorCode = ErrorCode.NotExist;
                debugMessage = "System Not Exist ";
                return false;
            }
        }
    }
}
