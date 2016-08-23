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
    public class StatusEffectManager
    {
        protected Dictionary<int, StatusEffect> statusEffectDictionary;

        public StatusEffectManager()
        {
            statusEffectDictionary = new Dictionary<int, StatusEffect>();

            LoadStatusEffects(Database.Database.RepositoryList.KnowledgeRepositoryList.StatusEffectRepository.List());
        }

        public bool ContainsStatusEffect(int statusEffectID)
        {
            return statusEffectDictionary.ContainsKey(statusEffectID);
        }
        public StatusEffect FindStatusEffect(int statusEffectID)
        {
            if (ContainsStatusEffect(statusEffectID))
            {
                return statusEffectDictionary[statusEffectID];
            }
            else
            {
                return null;
            }
        }
        public void LoadStatusEffect(StatusEffect statusEffect)
        {
            if (!ContainsStatusEffect(statusEffect.StatusEffectID))
            {
                statusEffectDictionary.Add(statusEffect.StatusEffectID, statusEffect);
            }
        }
        public void LoadStatusEffects(List<StatusEffect> statusEffects)
        {
            foreach (StatusEffect statusEffect in statusEffects)
            {
                if (!ContainsStatusEffect(statusEffect.StatusEffectID))
                {
                    statusEffectDictionary.Add(statusEffect.StatusEffectID, statusEffect);
                }
            }
        }
    }
}
