using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library.General.LightComponents.Knowledge
{
    public interface SkillKnowledgeInterface
    {
        bool OperateSkill(Soul soul, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage);
    }
}
