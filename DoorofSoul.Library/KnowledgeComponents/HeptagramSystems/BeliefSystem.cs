using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.MindComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.KnowledgeComponents.HeptagramSystems
{
    public class BeliefSystem : HeptagramSystem
    { 
        public override HeptagramSystemTypeCode SystemTypeCode { get { return HeptagramSystemTypeCode.Belief; } }

        public override bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if (base.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage))
            {
                if((SkillIDCode)skillInfo.Skill.SkillID == SkillIDCode.ObserverEye)
                {
                    if(user.Attributes.SpiritPoint >= skillInfo.Skill.BasicSpiritPointCost)
                    {
                        user.Attributes.SpiritPoint -= skillInfo.Skill.BasicSpiritPointCost;
                        if(agent.Entity.LocatedScene.SceneEye.Observer != agent)
                        {
                            StatusEffect statusEffect = Hexagram.Knowledge.StatusEffectManager.FindStatusEffect(1);
                            ContainerStatusEffectInfo statusEffectInfo = Database.Database.RepositoryList.KnowledgeRepositoryList.StatusEffectsRepositoryList.ContainerStatusEffectInfoRepository.Create(agent.ContainerID, statusEffect, statusEffect.StandardEffectDuration, DateTime.Now + new TimeSpan(7, 0, 0, 0));
                            agent.ContainerStatusEffectManager.LoadStatusEffectInfo(statusEffectInfo);
                            agent.Entity.LocatedScene.SceneEye.StopMonitor();
                            agent.Entity.LocatedScene.SceneEye.SetObserver(skillInfo.SkillLevel, agent);
                            agent.Entity.LocatedScene.SceneEye.StartMonitor(1000);
                            skillResponseParameters = new Dictionary<byte, object>();
                            debugMessage = "";
                            return true;
                        }
                        else
                        {
                            debugMessage = "Alreay be the observer";
                            return false;
                        }
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
                    debugMessage = string.Format("Skill Not Exist SkillID: {0}", skillInfo.Skill.SkillID);
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
