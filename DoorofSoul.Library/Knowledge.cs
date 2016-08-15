using DoorofSoul.Database;
using DoorofSoul.Library.General;
using DoorofSoul.Library.General.HeptagramSystems;
using DoorofSoul.Protocol;

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

            AlchemySystem.LoadSkills(DataBase.Instance.RepositoryManager.AlchemySkillRepository.List());
            ElementSystem.LoadSkills(DataBase.Instance.RepositoryManager.ElementSkillRepository.List());
            GenieSystem.LoadSkills(DataBase.Instance.RepositoryManager.GenieSkillRepository.List());
            DemonSystem.LoadSkills(DataBase.Instance.RepositoryManager.DemonSkillRepository.List());
            ChanceSystem.LoadSkills(DataBase.Instance.RepositoryManager.ChanceSkillRepository.List());
            TechnologySystem.LoadSkills(DataBase.Instance.RepositoryManager.TechnologySkillRepository.List());
            BeliefSystem.LoadSkills(DataBase.Instance.RepositoryManager.BeliefSkillRepository.List());
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
    }
}
