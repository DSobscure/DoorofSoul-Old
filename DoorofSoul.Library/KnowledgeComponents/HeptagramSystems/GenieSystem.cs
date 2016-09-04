using DoorofSoul.Library.General.ElementComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using DoorofSoul.Protocol.Communication.SkillParameters.AlchemySystem;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.KnowledgeComponents.HeptagramSystems
{
    public class GenieSystem : HeptagramSystem
    {
        public override HeptagramSystemTypeCode SystemTypeCode { get { return HeptagramSystemTypeCode.Genie; } }

        public override bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if (base.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage))
            {
                try
                {
                    if ((SkillIDCode)skillInfo.Skill.SkillID == SkillIDCode.SustainableNPCSummon)
                    {
                        Hexagram.Nature.SceneManager.NonPlayerContainerManager.StartManage(agent.Entity.LocatedScene, user);
                        skillResponseParameters = new Dictionary<byte, object>();
                        return true;
                    }
                    else
                    {
                        skillResponseParameters = new Dictionary<byte, object>();
                        debugMessage = string.Format("Skill Not Exist SkillID: {0}", skillInfo.Skill.SkillID);
                        return false;
                    }
                }
                catch (InvalidCastException ex)
                {
                    Hexagram.Log.ErrorFormat("GenieSystem OperateSkill Invalid Cast SkillID: {0}", skillInfo.Skill.SkillID);
                    Hexagram.Log.Error(ex.Message);
                    Hexagram.Log.Error(ex.StackTrace);
                    return false;
                }
                catch (Exception ex)
                {
                    Hexagram.Log.Error(ex.Message);
                    Hexagram.Log.Error(ex.StackTrace);
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
