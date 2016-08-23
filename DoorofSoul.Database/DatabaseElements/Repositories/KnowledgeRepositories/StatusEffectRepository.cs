using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Protocol;
using System.Collections.Generic;

namespace DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories
{
    public abstract class StatusEffectRepository
    {
        public abstract StatusEffect Create(string statusEffectName, float standardEffectDuration);
        public abstract void Delete(int statusEffectID);
        public abstract StatusEffect Find(int statusEffectID);
        public abstract void Save(StatusEffect statusEffect);
        public abstract List<StatusEffect> List();
    }
}
