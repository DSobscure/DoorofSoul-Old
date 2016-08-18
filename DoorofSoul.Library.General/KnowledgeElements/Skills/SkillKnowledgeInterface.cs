using System;
using System.Collections.Generic;
using DoorofSoul.Protocol.Communication;

namespace DoorofSoul.Library.General.KnowledgeElements.Skills
{
    public interface SkillKnowledgeInterface
    {
        bool OperateSkill(Soul soul, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage);
    }
}
