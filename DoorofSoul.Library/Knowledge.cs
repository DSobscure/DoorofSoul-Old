using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.Skills;
using DoorofSoul.Library.General.NatureComponents;
using DoorofSoul.Library.General.ThroneComponents;
using DoorofSoul.Library.KnowledgeComponents.HeptagramSystems;
using DoorofSoul.Library.KnowledgeComponents;
using DoorofSoul.Protocol;
using DoorofSoul.Protocol.Communication;
using System.Collections.Generic;

namespace DoorofSoul.Library
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
    }
}
