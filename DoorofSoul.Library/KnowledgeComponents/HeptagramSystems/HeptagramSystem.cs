using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.KnowledgeComponents.HeptagramSystems
{
    public abstract class HeptagramSystem
    {
        public abstract HeptagramSystemTypeCode SystemTypeCode { get; }

        public virtual bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if(skillInfo.Skill.SystemTypeCode == SystemTypeCode)
            { 
                skillResponseParameters = null;
                errorCode = ErrorCode.NoError;
                debugMessage = null;
                return true;
            }
            else
            {
                skillResponseParameters = new Dictionary<byte, object>();
                errorCode = ErrorCode.InvalidOperation;
                debugMessage = "HeptagramSystem Not Correct";
                return false;
            }
        }
    }
}
