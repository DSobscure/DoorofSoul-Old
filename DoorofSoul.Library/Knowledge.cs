using DoorofSoul.Hexagram.KnowledgeComponents;

namespace DoorofSoul.Hexagram
{
    public class Knowledge
    {
        public SkillManager SkillManager { get; protected set; }
        public StatusEffectManager StatusEffectManager { get; protected set; }

        public Knowledge()
        {
            SkillManager = new SkillManager();
            StatusEffectManager = new StatusEffectManager();
        }
        public void Initial()
        {
            SkillManager.Initial();
            StatusEffectManager.Initial();
        }
    }
}
