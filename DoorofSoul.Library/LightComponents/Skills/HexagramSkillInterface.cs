using DoorofSoul.Hexagram.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.LightComponents.Knowledge;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.LightComponents.Skills
{
    public class HexagramSkillInterface : SkillKnowledgeInterface
    {
        public bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            HeptagramSystem system = Hexagram.Instance.Knowledge.SkillManager.ChoseSystem(skillInfo.Skill.SystemTypeCode);
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
