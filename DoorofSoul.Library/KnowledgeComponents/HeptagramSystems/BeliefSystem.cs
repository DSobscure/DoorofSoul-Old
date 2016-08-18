using DoorofSoul.Library.General.KnowledgeComponents.Skill;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library.KnowledgeComponents.HeptagramSystems
{
    public class BeliefSystem : HeptagramSystem
    { 
        public override HeptagramSystemTypeCode SystemTypeCode { get { return HeptagramSystemTypeCode.Belief; } }

        public override bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if (base.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage))
            {
                if(skillInfo.Skill.SkillID == 1)
                {
                    if(user.Attributes.SpiritPoint >= skillInfo.Skill.BasicSpiritPointCost)
                    {
                        user.Attributes.SpiritPoint -= skillInfo.Skill.BasicSpiritPointCost;
                        agent.Entity.LocatedScene.SceneEye.StopMonitor();
                        agent.Entity.LocatedScene.SceneEye.SetObserver(skillInfo.SkillLevel, agent);
                        agent.Entity.LocatedScene.SceneEye.StartMonitor(1000);
                        skillResponseParameters = new Dictionary<byte, object>();
                        debugMessage = "";
                    }
                    else
                    {
                        errorCode = ErrorCode.InvalidOperation;
                        debugMessage = "SP Not Enough";
                        return false;
                    }
                }
                else
                {
                    skillResponseParameters = new Dictionary<byte, object>();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
