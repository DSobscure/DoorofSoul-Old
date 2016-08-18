using DoorofSoul.Database.DatabaseElements.Repositories.Knowledge.Skills;

namespace DoorofSoul.Database.DatabaseElements.RepositoryList.Knowledge
{
    public class SkillsList
    {
        public AlchemySkillRepository AlchemySkillRepository { get; protected set; }
        public ElementSkillRepository ElementSkillRepository { get; protected set; }
        public GenieSkillRepository GenieSkillRepository { get; protected set; }
        public DemonSkillRepository DemonSkillRepository { get; protected set; }
        public ChanceSkillRepository ChanceSkillRepository { get; protected set; }
        public TechnologySkillRepository TechnologySkillRepository { get; protected set; }
        public BeliefSkillRepository BeliefSkillRepository { get; protected set; }

        public SkillInfoRepository SkillInfoRepository { get; protected set; }
    }
}
