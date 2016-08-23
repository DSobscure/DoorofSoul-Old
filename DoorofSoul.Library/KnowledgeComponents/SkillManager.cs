using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library.KnowledgeComponents
{
    public class SkillManager
    {
        protected Dictionary<int, Skill> skillDictionary;

        public AlchemySystem AlchemySystem { get; protected set; }
        public ElementSystem ElementSystem { get; protected set; }
        public GenieSystem GenieSystem { get; protected set; }
        public DemonSystem DemonSystem { get; protected set; }
        public ChanceSystem ChanceSystem { get; protected set; }
        public TechnologySystem TechnologySystem { get; protected set; }
        public BeliefSystem BeliefSystem { get; protected set; }

        public SkillManager()
        {
            skillDictionary = new Dictionary<int, Skill>();

            LoadSkills(Database.Database.RepositoryList.KnowledgeRepositoryList.SkillRepository.List());

            AlchemySystem = new AlchemySystem();
            ElementSystem = new ElementSystem();
            GenieSystem = new GenieSystem();
            DemonSystem = new DemonSystem();
            ChanceSystem = new ChanceSystem();
            TechnologySystem = new TechnologySystem();
            BeliefSystem = new BeliefSystem();
        }

        public bool ContainsSkill(int skillID)
        {
            return skillDictionary.ContainsKey(skillID);
        }
        public Skill FindSkill(int skillID)
        {
            if (ContainsSkill(skillID))
            {
                return skillDictionary[skillID];
            }
            else
            {
                return null;
            }
        }
        public void LoadSkill(Skill skill)
        {
            if (!ContainsSkill(skill.SkillID))
            {
                skillDictionary.Add(skill.SkillID, skill);
            }
        }
        public void LoadSkills(List<Skill> skills)
        {
            foreach (Skill skill in skills)
            {
                if (!ContainsSkill(skill.SkillID))
                {
                    skillDictionary.Add(skill.SkillID, skill);
                }
            }
        }
        public HeptagramSystem ChoseSystem(HeptagramSystemTypeCode systemTypeCode)
        {
            switch (systemTypeCode)
            {
                case HeptagramSystemTypeCode.Alchemy:
                    return AlchemySystem;
                case HeptagramSystemTypeCode.Element:
                    return ElementSystem;
                case HeptagramSystemTypeCode.Genie:
                    return GenieSystem;
                case HeptagramSystemTypeCode.Demon:
                    return DemonSystem;
                case HeptagramSystemTypeCode.Chance:
                    return ChanceSystem;
                case HeptagramSystemTypeCode.Technology:
                    return TechnologySystem;
                case HeptagramSystemTypeCode.Belief:
                    return BeliefSystem;
                default:
                    return null;
            }
        }
        public bool OperateSkill(Soul user, Container agent, SkillInfo skillInfo, Dictionary<byte, object> skillParameters, out Dictionary<byte, object> skillResponseParameters, out ErrorCode errorCode, out string debugMessage)
        {
            if (ContainsSkill(skillInfo.SkillInfoID))
            {
                HeptagramSystem system = ChoseSystem(skillInfo.Skill.SystemTypeCode);
                if (system != null)
                {
                    return system.OperateSkill(user, agent, skillInfo, skillParameters, out skillResponseParameters, out errorCode, out debugMessage);
                }
                else
                {
                    skillResponseParameters = new Dictionary<byte, object>();
                    errorCode = ErrorCode.NotExist;
                    debugMessage = "Not Exist System";
                    return false;
                }
            }
            else
            {
                skillResponseParameters = new Dictionary<byte, object>();
                errorCode = ErrorCode.NotExist;
                debugMessage = "SkillInfo Not Exist";
                return false;
            }
        }
    }
}
