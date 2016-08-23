using DoorofSoul.Library.General.KnowledgeComponents;
using DoorofSoul.Library.General.KnowledgeComponents.StatusEffects;
using DoorofSoul.Protocol;
using System.Collections.Generic;
using System;

namespace DoorofSoul.Database.DatabaseElements.Repositories.KnowledgeRepositories.StatusEffectsRepositories
{
    public abstract class ContainerStatusEffectInfoRepository
    {
        public abstract ContainerStatusEffectInfo Create(int affectedContainerID, StatusEffect statusEffect, float effectDuration, DateTime expirationTime);
        public abstract void Delete(int containerStatusEffectInfoID);
        public abstract ContainerStatusEffectInfo Find(int containerStatusEffectInfoID);
        public abstract void Save(ContainerStatusEffectInfo containerStatusEffectInfo);
        public abstract List<ContainerStatusEffectInfo> ListOfAffected(int affectedContainerID);
    }
}
