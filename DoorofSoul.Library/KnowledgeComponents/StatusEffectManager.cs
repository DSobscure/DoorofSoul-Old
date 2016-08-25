using DoorofSoul.Library.General.KnowledgeComponents;
using System.Collections.Generic;

namespace DoorofSoul.Hexagram.KnowledgeComponents
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
