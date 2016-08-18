using DoorofSoul.Database;
using DoorofSoul.Library.General.KnowledgeComponents.Skill;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library
{
    public class Knowledge
    {
        public AlchemySystem AlchemySystem { get; protected set; }
        public ElementSystem ElementSystem { get; protected set; }
        public GenieSystem GenieSystem { get; protected set; }
        public DemonSystem DemonSystem { get; protected set; }
        public ChanceSystem ChanceSystem { get; protected set; }
        public TechnologySystem TechnologySystem { get; protected set; }
        public BeliefSystem BeliefSystem { get; protected set; }

        public Knowledge()
        {
            AlchemySystem = new AlchemySystem();
            ElementSystem = new ElementSystem();
            GenieSystem = new GenieSystem();
            DemonSystem = new DemonSystem();
            ChanceSystem = new ChanceSystem();
            TechnologySystem = new TechnologySystem();
            BeliefSystem = new BeliefSystem();

            AlchemySystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.AlchemySkillRepository.List());
            ElementSystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.ElementSkillRepository.List());
            GenieSystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.GenieSkillRepository.List());
            DemonSystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.DemonSkillRepository.List());
            ChanceSystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.ChanceSkillRepository.List());
            TechnologySystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.TechnologySkillRepository.List());
            BeliefSystem.LoadSkills(DataBase.Instance.RepositoryManager.KnowledgeList.SkillsList.BeliefSkillRepository.List());
        }

        public HeptagramSystem ChoseSystem(HeptagramSystemTypeCode systemTypeCode)
        {
            switch(systemTypeCode)
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
            HeptagramSystem system = ChoseSystem(skillInfo.Skill.SystemTypeCode);
            if(system != null)
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
    }
}
